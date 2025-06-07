/*import React, { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, PieChart, Pie, Cell } from 'recharts';
import './Dashboard.css'; // Nosso novo arquivo CSS simples

// --- Interfaces de Dados (baseadas na sua API) ---
interface ItemEstoque {
  id: string;
  descricao: string;
  quantidade: number;
  // preco: number; // Se precisar para outros cards
}

interface Usuario { // Supondo que sua API de usuários retornará algo assim
  id: string;
  nomeCompleto: string;
  ativo: boolean; // CAMPO ESSENCIAL para o gráfico de status
}

// --- Configuração da API ---
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:5004'; // Do seu .env

const getToken = () => localStorage.getItem('userToken'); // Ou de onde o token é armazenado

// --- Componente Dashboard ---
const Dashboard: React.FC = () => {
  const [itemChartData, setItemChartData] = useState<{ name: string; quantidade: number }[]>([]);
  const [userStatusChartData, setUserStatusChartData] = useState<{ name: string; value: number }[]>([]);
  const [totalTiposItens, setTotalTiposItens] = useState(0);
  const [totalEstoqueItens, setTotalEstoqueItens] = useState(0);
  const [totalUsuarios, setTotalUsuarios] = useState(0); // Exemplo de outro card

  const [loadingItens, setLoadingItens] = useState(true);
  const [loadingUsuarios, setLoadingUsuarios] = useState(true);
  const [errorApi, setErrorApi] = useState<string | null>(null);

  // Efeito para buscar dados dos itens
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

  // Efeito para buscar dados dos usuários (placeholder)
  useEffect(() => {
    const fetchUsuariosData = async () => {
      setLoadingUsuarios(true);
      // !!! ATENÇÃO: API PARA USUÁRIOS PRECISA SER DEFINIDA !!!
      // Você precisará de um endpoint como GET /api/usuario que retorne List<Usuario>
      // e cada Usuario deve ter um campo 'ativo: boolean'.
      try {
         const token = getToken();
         const response = await fetch(`${API_BASE_URL}/api/usuario`, { // Endpoint de exemplo
           headers: {
             'Authorization': `Bearer ${token}`,
           },
         });
         if (!response.ok) {
           throw new Error(`Erro HTTP ${response.status} ao buscar usuários.`);
         }
         const data: Usuario[] = await response.json();

        // Mock data enquanto a API não está pronta:
        const mockData: Usuario[] = [
          { id: '1', nomeCompleto: 'Usuário A', ativo: true },
          { id: '2', nomeCompleto: 'Usuário B', ativo: false },
          { id: '3', nomeCompleto: 'Usuário C', ativo: true },
          { id: '4', nomeCompleto: 'Usuário D', ativo: true },
        ];
        const data = mockData; // REMOVER QUANDO API ESTIVER PRONTA E DESCOMENTAR FETCH ACIMA

        let ativos = 0;
        let inativos = 0;
        data.forEach(user => {
          if (user.ativo) ativos++;
          else inativos++;
        });
        setUserStatusChartData([
          { name: 'Ativos', value: ativos },
          { name: 'Inativos', value: inativos },
        ]);
        setTotalUsuarios(data.length);
        // setErrorApi(null); // Descomentar quando a API real for usada
      } catch (error: any) {
        console.error("Falha ao buscar dados de usuários:", error);
        // setErrorApi(error.message || "Falha ao carregar dados de usuários."); // Descomentar
      } finally {
        setLoadingUsuarios(false);
      }
    };

    // Se o endpoint não existir, você pode comentar a chamada abaixo para evitar erros no console
    // ou criar um endpoint mockado no seu backend para testes.
    // Por enquanto, estou usando dados mockados.
    fetchUsuariosData();
  }, []);


  const PIE_COLORS = ['#00C49F', '#FF8042']; // Verde para ativos, Laranja para inativos

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
        {/* Adicione mais cards de resumo aqui, se necessário */}
      </section>

      <section className="charts-section">
        <div className="chart-card">
          <h3>Estoque por Item</h3>
          {loadingItens ? <p>Carregando gráfico de estoque...</p> : itemChartData.length > 0 ? (
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
          ) : <p>Sem dados de estoque para exibir.</p>}
        </div>

        <div className="chart-card">
          <h3>Status de Usuários</h3>
          {loadingUsuarios ? <p>Carregando gráfico de usuários...</p> : userStatusChartData.length > 0 ? (
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
          ) : <p>Sem dados de status de usuários para exibir. Verifique a API.</p>}
        </div>
      </section>
    </div>
  );
};

export default Dashboard;