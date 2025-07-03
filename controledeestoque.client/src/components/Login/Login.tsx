// src/components/Login/Login.tsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Login.css';
import api from '../../axios';

interface LoginProps {
    cpf: string;
    senha: string;
}

const Login: React.FC = () => {
    const [formData, setFormData] = useState<LoginProps>({ cpf: '', senha: '' });
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();

    // A URL base da API deve vir do .env
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:44324'; // Fallback se n�o definido

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await api.post('api/usuario/login', formData);
            setFormData({
                cpf: '',
                senha: ''
            });

            const token = await response.data.token;

            localStorage.setItem('token', token);

            alert('Login realizado com sucesso!');
            navigate('/dashboard');
        } catch (err: any) {
            setError(err.response.data);
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
                    <label htmlFor="username">Usu�rio (CPF)</label> {/* Melhorar htmlFor */}
                    <input
                        type="text"
                        name="cpf"
                        value={formData.cpf}
                        onChange={handleChange}
                        required
                    />
                </div> {/* Fechar div.form-group aqui */}
                <div className="form-group"> {/* Outro form-group para a senha */}
                    <label htmlFor="password">Senha</label> {/* Melhorar htmlFor */}
                    <input
                        type="password"
                        name="senha"
                        value={formData.senha}
                        onChange={handleChange}
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