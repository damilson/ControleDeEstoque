import React, { useState } from 'react';
import './Login.css';

interface LoginProps {
    username: string;
    password: string;
}

const Login: React.FC = () => {
    const [formData, setFormData] = useState<LoginProps>({ username: '', password: '' });
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await fetch('https://localhost:44321/api/usuario/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    cpf: formData.username,
                    senha: formData.password,
                }),
            });

            if (!response.ok) {
                if (response.status === 401) {
                    throw new Error('Usuário ou senha inválidos.');
                } else {
                    throw new Error('Erro ao conecetar ao servidor.');
                }
            }

            //const data = await response.json();
            alert('Login realizado com sucesso!');
        } catch (err: any) {
            setError(err.message);
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
                    <label>Usuario</label>
                    <input
                        type="text"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        required
                    />
                    <label>Senha</label>
                    <input
                        type="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? 'Carregando...' : 'Entrar'}
                </button>
                {error && <div className="error">{error}</div>}
            </form>
        </div>
    );
};

export default Login;