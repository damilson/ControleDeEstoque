using AutoMapper;
using Entidades;
using Repositorios.Contexto;
using Repositorios.Generico;
using Servicos.Interfaces;

namespace Servicos
{
    public abstract class ServicoBase<IBaseDTO, TEntity> : IServicoBase<IBaseDTO, TEntity>
        where TEntity : class, IEntity
        where IBaseDTO : class
    {
        protected readonly IMapper _mapper;
        protected readonly RepositorioGenerico<TEntity> _repositorio;
        protected readonly Contexto _contexto;

        public ServicoBase(Contexto contexto, IMapper mapper)
        {
            _mapper = mapper;
            _repositorio = new RepositorioGenerico<TEntity>(contexto);
            _contexto = contexto;
        }

        public virtual async Task<Guid> IncluirAsync(IBaseDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var created = await _repositorio.CreatedAsync(entity);
            return created.Id;
        }

        public virtual async Task<IBaseDTO> IncluirEntityAsync(IBaseDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var created = await _repositorio.CreatedAsync(entity);
            return _mapper.Map<IBaseDTO>(created);
        }

        public virtual async Task RemoverAsync(Guid id)
        {
            await _repositorio.DeleteAsync(id);
        }

        public virtual async Task<IBaseDTO> BuscarPorIdAsync(Guid id)
        {
            var entity = await _repositorio.GetByIdAsync(id);
            return _mapper.Map<IBaseDTO>(entity);
        }

        public virtual async Task<List<IBaseDTO>> BuscarAsync()
        {
            var list = await _repositorio.GetAllAsync();
            return _mapper.Map<List<IBaseDTO>>(list);
        }

        public virtual async Task<Guid> AtualizarAsync(IBaseDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updated = await _repositorio.UpdateAsync(entity);
            return updated.Id;
        }
        
        public abstract Task ValidarOperacao(IBaseDTO model);
    }
}