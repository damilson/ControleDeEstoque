import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:44321/api', // ajuste conforme necessário
});

api.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

api.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;

        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            try {
                const refreshToken = localStorage.getItem('refreshToken');
                if (!refreshToken) throw new Error('Sem refresh token');

                const response = await api.post('/auth/refresh-token', { refreshToken });
                const { accessToken, refreshToken: newRefresh } = response.data;

                localStorage.setItem('token', accessToken);
                localStorage.setItem('refreshToken', newRefresh);

                originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                return api(originalRequest);
            } catch (e) {
                localStorage.clear();
                window.location.href = '/';
            }
        }

        return Promise.reject(error);
    }
);

export default api;
