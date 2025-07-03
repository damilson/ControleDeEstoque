using AutoMapper;
using Entidades;
using Repositorios.Contexto;
using Repositorios.Generico;
using Servicos.DTO;
using Servicos.Interfaces;

namespace Servicos
{
    public class PedidosServico : ServicoBase<PedidosDTO, Pedidos>, IPedidosServico
    {
        private readonly IRepositorioGenerico<Itens> _repositorioItens;
        private readonly IRepositorioGenerico<PedidosItens> _repositorioPedidosItens;

        public PedidosServico(Contexto contexto, IMapper mapper) : base(contexto, mapper)
        {
            _repositorioItens = new RepositorioGenerico<Itens>(contexto);
            _repositorioPedidosItens = new RepositorioGenerico<PedidosItens>(contexto);
        }

        public async Task<Guid> FecharPedido(Guid pedidoId)
        {
            try
            {
                var pedido = await _repositorio.GetByIdAsync(pedidoId);

                if (pedido.SituacaoPedido == Util.SituacaoPedido.FECHADO)
                    throw new Exception($"Pedidos com status de fechado, não podem ser alterados.");

                if (pedido == null)
                {
                    throw new Exception("Pedido não encontrado.");
                }

                foreach (var item in pedido.Itens)
                {
                    var itemEstoque = await _repositorioItens.GetByIdAsync(item.ItemId);
                    if (itemEstoque == null)
                    {
                        throw new Exception($"Item '{item.Item.Descricao}' não encontrado no estoque.");
                    }

                    if (itemEstoque.Quantidade < item.Quantidade)
                    {
                        throw new Exception($"Estoque insuficiente para o item '{item.Item.Descricao}'.");

                    }
                }

                foreach (var item in pedido.Itens)
                {
                    var itemEstoque = await _repositorioItens.GetByIdAsync(item.ItemId);
                    itemEstoque.Quantidade -= item.Quantidade;
                    await _repositorioItens.UpdateAsync(itemEstoque);
                }

                pedido.SituacaoPedido = Util.SituacaoPedido.FECHADO;
                await _repositorio.UpdateAsync(pedido);

                return pedidoId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override async Task<Guid> AtualizarAsync(PedidosDTO dto)
        {
            try
            {
                var pedido = await _repositorio.GetByIdAsync(dto.Id);

                if (pedido.SituacaoPedido == Util.SituacaoPedido.FECHADO)
                    throw new Exception($"Pedidos com status de fechado, não podem ser alterados.");

                await ValidarOperacao(dto);
                return await base.AtualizarAsync(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ReabrirPedido(Guid id)
        {
            var pedido = await _repositorio.GetByIdAsync(id);
            pedido.SituacaoPedido = Util.SituacaoPedido.ABERTO;
            
            await _repositorio.UpdateAsync(pedido);
        }

        public async Task IncluirItemPedido(PedidosItensDTO pedidoItens)
        {
            var pedido = await _repositorio.GetByIdAsync(pedidoItens.PedidoId);

            if (pedido.SituacaoPedido == Util.SituacaoPedido.FECHADO)
                throw new Exception($"Pedidos com status de fechado, não podem ser alterados.");

            await _repositorioPedidosItens.CreatedAsync(_mapper.Map<PedidosItens>(pedidoItens));
        }

        public async Task RemoverItemPedido(Guid itemId, Guid pedidoId)
        {
            try
            {
                var pedido = await _repositorio.GetByIdAsync(pedidoId);

                if (pedido.SituacaoPedido == Util.SituacaoPedido.FECHADO)
                    throw new Exception($"Pedidos com status de fechado, não podem ser alterados.");

                await _repositorioPedidosItens.DeleteAsync(pedidoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override async Task ValidarOperacao(PedidosDTO model)
        {
            foreach (var item in model.Itens)
            {
                var itemEstoque = await _repositorioItens.GetByIdAsync(item.ItemId);
                if (itemEstoque == null)
                {
                    throw new Exception($"Item '{item.ItemId}' não encontrado no estoque.");
                }

                if (itemEstoque.Quantidade < item.Quantidade)
                {
                    throw new Exception($"Estoque insuficiente para o item '{itemEstoque.Descricao}'.");

                }
            }
        }
    }
}
