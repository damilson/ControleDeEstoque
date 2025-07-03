// src/components/Dashboard.tsx
import { useNavigate } from 'react-router-dom';
import api from '../../axios';

const Dashboard = () => {
    const navigate = useNavigate();

    const logout = () => {
        const refreshToken = localStorage.getItem('refreshToken');
        api.post('/auth/logout', { refreshToken }).finally(() => {
            localStorage.clear();
            navigate('/');
        });
    };

    return (
        <div>
            <h1>Bem-vindo!</h1>
            <button onClick={logout}>Sair</button>
        </div>
    );
};

export default Dashboard;
