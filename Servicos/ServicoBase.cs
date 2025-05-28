using AutoMapper;
using Entidades;
using Repositorios.Contexto;
using Repositorios.Generico;
using Servicos.Interfaces;

namespace Servicos
{
    public class ServicoBase<IBaseDTO, TEntity> : RepositorioGenerico<TEntity>, IServicoBase<IBaseDTO, TEntity> where TEntity : class, IEntity where IBaseDTO : class
    {
        protected readonly IMapper _mapper;

        public ServicoBase(Contexto contexto, IMapper mapper) : base(contexto) => _mapper = mapper;

        public virtual async Task<Guid> IncluirAsync(IBaseDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var created = await CreatedAsync(entity);
            return created.Id;
        }

        public virtual async Task<IBaseDTO> IncluirEntityAsync(IBaseDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var created = await CreatedAsync(entity);
            return _mapper.Map<IBaseDTO>(created);
        }

        public virtual async Task RemoverAsync(Guid id)
        {
            await DeleteAsync(id);
        }

        public virtual async Task<IBaseDTO> BuscarPorIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            return _mapper.Map<IBaseDTO>(entity);
        }

        public virtual async Task<List<IBaseDTO>> BuscarAsync()
        {
            var list = await GetAllAsync();
            return _mapper.Map<List<IBaseDTO>>(list);
        }

        public virtual async Task<Guid> AtualizarAsync(IBaseDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updated = await UpdateAsync(entity);
            return updated.Id;
        }
    }
}