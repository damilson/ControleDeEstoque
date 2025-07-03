import React, { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, PieChart, Pie, Cell } from 'recharts';
import './Dashboard.css';

// Dados mockados para teste
const MOCK_ITEMS: ItemEstoque[] = [
    { id: '1', descricao: 'Notebook', quantidade: 15 },
    { id: '2', descricao: 'Mouse', quantidade: 42 },
    { id: '3', descricao: 'Teclado', quantidade: 30 },
    { id: '4', descricao: 'Monitor', quantidade: 18 },
];

const MOCK_USERS: Usuario[] = [
    { id: '1', nomeCompleto: 'João Silva', ativo: true },
    { id: '2', nomeCompleto: 'Maria Souza', ativo: true },
    { id: '3', nomeCompleto: 'Carlos Oliveira', ativo: false },
];

interface ItemEstoque {
    id: string;
    descricao: string;
    quantidade: number;
}

interface Usuario {
    id: string;
    nomeCompleto: string;
    ativo: boolean;
}

const Dashboard: React.FC = () => {
    const [itemChartData, setItemChartData] = useState<{ name: string; quantidade: number }[]>([]);
    const [userStatusChartData, setUserStatusChartData] = useState<{ name: string; value: number }[]>([]);
    const [totalTiposItens, setTotalTiposItens] = useState(0);
    const [totalEstoqueItens, setTotalEstoqueItens] = useState(0);
    const [totalUsuarios, setTotalUsuarios] = useState(0);

    useEffect(() => {
        // Mock: Processamento dos itens
        const chartData = MOCK_ITEMS.map(item => ({
            name: item.descricao,
            quantidade: item.quantidade,
        }));

        setItemChartData(chartData);
        setTotalTiposItens(MOCK_ITEMS.length);
        setTotalEstoqueItens(MOCK_ITEMS.reduce((sum, item) => sum + item.quantidade, 0));
    }, []);

    useEffect(() => {
        // Mock: Processamento dos usuários
        const ativos = MOCK_USERS.filter(user => user.ativo).length;
        const inativos = MOCK_USERS.length - ativos;

        setUserStatusChartData([
            { name: 'Ativos', value: ativos },
            { name: 'Inativos', value: inativos },
        ]);
        setTotalUsuarios(MOCK_USERS.length);
    }, []);

    const PIE_COLORS = ['#00C49F', '#FF8042'];

    return (
        <div className="dashboard-container">
            <header className="dashboard-header">
                <h1>Dashboard de Controle de Estoque (Modo Teste)</h1>
                <p className="demo-warning">⚠️ Dados de demonstração - Não conectado à API</p>
            </header>

            <section className="summary-cards">
                <div className="card">
                    <h2>Tipos de Itens</h2>
                    <p className="stat">{totalTiposItens}</p>
                </div>
                <div className="card">
                    <h2>Estoque Total</h2>
                    <p className="stat">{totalEstoqueItens}</p>
                </div>
                <div className="card">
                    <h2>Total de Usuários</h2>
                    <p className="stat">{totalUsuarios}</p>
                </div>
            </section>

            <section className="charts-section">
                <div className="chart-card">
                    <h3>Estoque por Item</h3>
                    <ResponsiveContainer width="100%" height={300}>
                        <BarChart data={itemChartData}>
                            <CartesianGrid strokeDasharray="3 3" />
                            <XAxis dataKey="name" angle={-25} textAnchor="end" interval={0} height={60} />
                            <YAxis allowDecimals={false} />
                            <Tooltip />
                            <Legend />
                            <Bar dataKey="quantidade" fill="#8884d8" name="Quantidade" />
                        </BarChart>
                    </ResponsiveContainer>
                </div>

                <div className="chart-card">
                    <h3>Status de Usuários</h3>
                    <ResponsiveContainer width="100%" height={300}>
                        <PieChart>
                            <Pie
                                data={userStatusChartData}
                                cx="50%"
                                cy="50%"
                                labelLine={false}
                                label={({ name, percent }) => `${name}: ${(percent * 100).toFixed(0)}%`}
                                outerRadius={100}
                                fill="#8884d8"
                                dataKey="value"
                            >
                                {userStatusChartData.map((entry, index) => (
                                    <Cell key={`cell-${index}`} fill={PIE_COLORS[index % PIE_COLORS.length]} />
                                ))}
                            </Pie>
                            <Tooltip />
                            <Legend />
                        </PieChart>
                    </ResponsiveContainer>
                </div>
            </section>
        </div>
    );
};

export default Dashboard;