/* CASO USE API USAR O CÓDIGO ABAIXO



/*import React, { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, PieChart, Pie, Cell } from 'recharts';
import './Dashboard.css';

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

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:5004';

const getToken = () => localStorage.getItem('userToken');

const Dashboard: React.FC = () => {
    const [itemChartData, setItemChartData] = useState<{ name: string; quantidade: number }[]>([]);
    const [userStatusChartData, setUserStatusChartData] = useState<{ name: string; value: number }[]>([]);
    const [totalTiposItens, setTotalTiposItens] = useState(0);
    const [totalEstoqueItens, setTotalEstoqueItens] = useState(0);
    const [totalUsuarios, setTotalUsuarios] = useState(0);
    const [loadingItens, setLoadingItens] = useState(true);
    const [loadingUsuarios, setLoadingUsuarios] = useState(true);
    const [errorApi, setErrorApi] = useState<string | null>(null);

    useEffect(() => {
        const fetchItensData = async () => {
            setLoadingItens(true);
            try {
                const token = getToken();
                const response = await fetch(`${API_BASE_URL}/api/Itens`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                    },
                });
                if (!response.ok) {
                    throw new Error(`Erro HTTP ${response.status} ao buscar itens.`);
                }
                const data: ItemEstoque[] = await response.json();

                const chartData = data.map(item => ({
                    name: item.descricao,
                    quantidade: item.quantidade,
                }));
                setItemChartData(chartData);
                setTotalTiposItens(data.length);
                setTotalEstoqueItens(data.reduce((sum, item) => sum + item.quantidade, 0));
                setErrorApi(null);
            } catch (error: any) {
                console.error("Falha ao buscar dados de itens:", error);
                setErrorApi(error.message || "Falha ao carregar dados de itens.");
            } finally {
                setLoadingItens(false);
            }
        };

        fetchItensData();
    }, []);

    useEffect(() => {
        const fetchUsuariosData = async () => {
            setLoadingUsuarios(true);
            try {
                // Opção 1: Usar API real (descomente esta seção quando a API estiver pronta)
                /*
                const token = getToken();
                const response = await fetch(`${API_BASE_URL}/api/usuario`, {
                  headers: {
                    'Authorization': `Bearer ${token}`,
                  },
                });
                if (!response.ok) {
                  throw new Error(`Erro HTTP ${response.status} ao buscar usuários.`);
                }
                const data: Usuario[] = await response.json();
                */

                // Opção 2: Dados mockados (remova esta seção quando a API estiver pronta)
                const data: Usuario[] = [
                    { id: '1', nomeCompleto: 'Usuário A', ativo: true },
                    { id: '2', nomeCompleto: 'Usuário B', ativo: false },
                    { id: '3', nomeCompleto: 'Usuário C', ativo: true },
                    { id: '4', nomeCompleto: 'Usuário D', ativo: true },
                ];

                const ativos = data.filter(user => user.ativo).length;
                const inativos = data.length - ativos;

                setUserStatusChartData([
                    { name: 'Ativos', value: ativos },
                    { name: 'Inativos', value: inativos },
                ]);
                setTotalUsuarios(data.length);
                setErrorApi(null);
            } catch (error: any) {
                console.error("Falha ao buscar dados de usuários:", error);
                setErrorApi(error.message || "Falha ao carregar dados de usuários.");
            } finally {
                setLoadingUsuarios(false);
            }
        };

        fetchUsuariosData();
    }, []);

    const PIE_COLORS = ['#00C49F', '#FF8042'];

    if (errorApi) {
        return <div className="dashboard-container error-message">Erro: {errorApi}</div>;
    }

    return (
        <div className="dashboard-container">
            <header className="dashboard-header">
                <h1>Dashboard de Controle de Estoque</h1>
            </header>

            <section className="summary-cards">
                <div className="card">
                    <h2>Tipos de Itens</h2>
                    <p className="stat">{loadingItens ? '...' : totalTiposItens}</p>
                </div>
                <div className="card">
                    <h2>Estoque Total</h2>
                    <p className="stat">{loadingItens ? '...' : totalEstoqueItens}</p>
                </div>
                <div className="card">
                    <h2>Total de Usuários</h2>
                    <p className="stat">{loadingUsuarios ? '...' : totalUsuarios}</p>
                </div>
            </section>

            <section className="charts-section">
                <div className="chart-card">
                    <h3>Estoque por Item</h3>
                    {loadingItens ? (
                        <p>Carregando gráfico de estoque...</p>
                    ) : itemChartData.length > 0 ? (
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
                    ) : (
                        <p>Sem dados de estoque para exibir.</p>
                    )}
                </div>

                <div className="chart-card">
                    <h3>Status de Usuários</h3>
                    {loadingUsuarios ? (
                        <p>Carregando gráfico de usuários...</p>
                    ) : userStatusChartData.length > 0 ? (
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
                    ) : (
                        <p>Sem dados de status de usuários para exibir.</p>
                    )}
                </div>
            </section>
        </div>
    );
};

export default Dashboard;