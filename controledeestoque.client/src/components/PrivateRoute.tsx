import type { JSX } from 'react';
import { Navigate } from 'react-router-dom';

const isAuthenticated = () => {
    const token = localStorage.getItem('token');
    return !!token;
};

const PrivateRoute = ({ children }: { children: JSX.Element }) => {
    return isAuthenticated() ? children : <Navigate to="/" />;
};

export default PrivateRoute;
