import React, { type ChangeEvent, type FormEvent, useEffect, useState } from 'react';
import axios from 'axios';
import api from '../../axios';

type PedidoItem = {
    id?: string;
    itemId: string;
    quantidade: number;
};

type Pedido = {
    id?: string;
    nomeCliente: string;
    numero: string;
    situacaoPedido: number;
    itens: PedidoItem[];
};

const PedidosPage: React.FC = () => {
    const [pedidos, setPedidos] = useState<Pedido[]>([]);
    const [form, setForm] = useState<Pedido>({
        nomeCliente: '',
        numero: '',
        situacaoPedido: 0,
        itens: [],
    });

    const fetchPedidos = async () => {
        try {
            const response = await api.get<Pedido[]>('/api/pedidos');
            setPedidos(response.data);
        } catch (error) {
            console.error('Erro ao buscar pedidos', error);
        }
    };

    const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setForm((prev) => ({
            ...prev,
            [name]: name === 'situacaoPedido' ? parseInt(value) : value,
        }));
    };

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            await api.post('/api/pedidos', form);
            setForm({
                nomeCliente: '',
                numero: '',
                situacaoPedido: 0,
                itens: [],
            });
            fetchPedidos();
        } catch (error) {
            console.error('Erro ao salvar pedido', error);
        }
    };

    const deletePedido = async (id?: string) => {
        if (!id) return;
        try {
            await api.delete(`/api/pedidos/${id}`);
            fetchPedidos();
        } catch (error) {
            console.error('Erro ao excluir pedido', error);
        }
    };

    useEffect(() => {
        fetchPedidos();
    }, []);

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h1 className="text-2xl font-bold mb-4">Cadastro de Pedidos</h1>

            <form onSubmit={handleSubmit} className="space-y-4 border p-4 rounded shadow">
                <input
                    type="text"
                    name="nomeCliente"
                    placeholder="Nome do Cliente"
                    value={form.nomeCliente}
                    onChange={handleChange}
                    className="w-full border p-2 rounded"
                    required
                />
                <input
                    type="text"
                    name="numero"
                    placeholder="Número do Pedido"
                    value={form.numero}
                    onChange={handleChange}
                    className="w-full border p-2 rounded"
                    required
                />
                <select
                    name="situacaoPedido"
                    value={form.situacaoPedido}
                    onChange={handleChange}
                    className="w-full border p-2 rounded"
                >
                    <option value={0}>ABERTO</option>
                    <option value={1}>PAGO</option>
                    <option value={2}>CANCELADO</option>
                </select>
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
                    Salvar Pedido
                </button>
            </form>

            <h2 className="text-xl font-semibold mt-8">Lista de Pedidos</h2>
            <table className="w-full mt-4 border">
                <thead className="bg-gray-200">
                    <tr>
                        <th className="p-2 border">Cliente</th>
                        <th className="p-2 border">Número</th>
                        <th className="p-2 border">Situação</th>
                        <th className="p-2 border">Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {pedidos.map((pedido) => (
                        <tr key={pedido.id}>
                            <td className="p-2 border">{pedido.nomeCliente}</td>
                            <td className="p-2 border">{pedido.numero}</td>
                            <td className="p-2 border">
                                {pedido.situacaoPedido === 0
                                    ? 'ABERTO'
                                    : pedido.situacaoPedido === 1
                                        ? 'PAGO'
                                        : 'CANCELADO'}
                            </td>
                            <td className="p-2 border">
                                <button
                                    className="text-red-500 hover:underline"
                                    onClick={() => deletePedido(pedido.id)}
                                >
                                    Excluir
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default PedidosPage;
