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

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

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
                    <label>Usuario</label>
                    <input
                        type="text"
                        name="cpf"
                        value={formData.cpf}
                        onChange={handleChange}
                        required
                    />
                    <label>Senha</label>
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
                {error && <div className="error">{error}</div>}
            </form>
        </div>
    );
};

export default Login;