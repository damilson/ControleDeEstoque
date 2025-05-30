using Repositorios.Generico;

namespace Servicos.Interfaces
{
    public interface IServicoBase<IBaseDTO, TEntity>
    {
        Task<Guid> IncluirAsync(IBaseDTO dto);

        Task<IBaseDTO> IncluirEntityAsync(IBaseDTO dto);

        Task RemoverAsync(Guid id);

        Task<IBaseDTO> BuscarPorIdAsync(Guid id);

        Task<List<IBaseDTO>> BuscarAsync();

        Task<Guid> AtualizarAsync(IBaseDTO dto);
    }
}
