import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const IDLE_TIMEOUT_MS = 60 * 60 * 1000; // 1 hora

const useIdleLogout = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const updateLastActivity = () => {
            localStorage.setItem('lastActivity', Date.now().toString());
        };

        const checkIdle = () => {
            const last = localStorage.getItem('lastActivity');
            const token = localStorage.getItem('token');
            if (token && last) {
                const elapsed = Date.now() - parseInt(last);
                if (elapsed > IDLE_TIMEOUT_MS) {
                    localStorage.clear();
                    alert('Sessão expirada por inatividade.');
                    navigate('/');
                }
            }
        };

        const events = ['click', 'mousemove', 'keydown', 'scroll'];
        events.forEach(e => window.addEventListener(e, updateLastActivity));

        updateLastActivity();
        const interval = setInterval(checkIdle, 60000);

        return () => {
            events.forEach(e => window.removeEventListener(e, updateLastActivity));
            clearInterval(interval);
        };
    }, [navigate]);
};

export default useIdleLogout;
