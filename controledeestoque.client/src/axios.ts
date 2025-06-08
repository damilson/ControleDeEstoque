// src/api/axios.ts
import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL, // ou process.env.REACT_APP_API_URL
});

export default api;
