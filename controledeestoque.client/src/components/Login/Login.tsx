// src/components/Login/Login.tsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; // 1. Importar useNavigate
import './Login.css';

// Interface LoginProps não é usada para o estado do formulário,
// o estado é inferido. Mas é bom para clareza se você quiser.
// interface LoginProps {
//     username: string; // Manteve username, mas a API espera 'cpf'
//     password: string;
// }

const Login: React.FC = () => {
    // formData agora usa diretamente os nomes dos campos do formulário
    const [username, setUsername] = useState(''); // Para o campo 'username' (que será o CPF)
    const [password, setPassword] = useState(''); // Para o campo 'password'
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate(); // 2. Inicializar useNavigate

    // A URL base da API deve vir do .env
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:44324'; // Fallback se não definido

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_BASE_URL}/api/usuario/login`, { // Usar API_BASE_URL
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    cpf: username, // Enviar 'username' como 'cpf' para a API
                    senha: password, // Enviar 'password' como 'senha'
                }),
            });

            if (!response.ok) {
                if (response.status === 400 || response.status === 401) { // 400 para "Bad Request" também pode ser usuário/senha inválido
                    // Tenta pegar uma mensagem de erro do corpo da resposta
                    const errorData = await response.json().catch(() => null);
                    throw new Error(errorData?.message || 'Usuário ou senha inválidos.');
                } else {
                    throw new Error(`Erro ao conectar ao servidor: ${response.status}`);
                }
            }

            const data = await response.json(); // 3. Descomentar e pegar os dados (que devem incluir o token)

            if (data && data.token) {
                localStorage.setItem('userToken', data.token); // 4. Salvar o token
                // alert('Login realizado com sucesso!'); // Pode remover ou manter se quiser
                navigate('/dashboard.tsx'); // 5. Redirecionar para o dashboard
            } else {
                throw new Error('Token não recebido do servidor.');
            }

        } catch (err: any) {
            setError(err.message || 'Ocorreu um erro desconhecido.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="login-container">
            <form className="login-form" onSubmit={handleSubmit}>
                <h2>Controle de Estoque</h2>
                <h3>Login</h3>
                <div className="form-group">
                    <label htmlFor="username">Usuário (CPF)</label> {/* Melhorar htmlFor */}
                    <input
                        type="text"
                        id="username" // Adicionar id para o label
                        name="username" // Mantido como 'username' para o estado local
                        value={username}
                        onChange={(e) => setUsername(e.target.value)} // Atualizar estado individualmente
                        required
                    />
                </div> {/* Fechar div.form-group aqui */}
                <div className="form-group"> {/* Outro form-group para a senha */}
                    <label htmlFor="password">Senha</label> {/* Melhorar htmlFor */}
                    <input
                        type="password"
                        id="password" // Adicionar id para o label
                        name="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)} // Atualizar estado individualmente
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? 'Carregando...' : 'Entrar'}
                </button>
                {error && <div className="error" style={{color: 'red', marginTop: '10px'}}>{error}</div>} {/* Estilo inline para destaque */}
            </form>
        </div>
    );
};

export default Login;