import { Routes, Route } from 'react-router-dom';
import Login from './components/Login/Login';
import PrivateRoute from './components/PrivateRoute';
import Dashboard from './components/Dashboard/Dashboard';
import './App.css';
import useIdleLogout from './hooks/useIdleLogout';

function App() {
    useIdleLogout();

    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route
                path="/dashboard"
                element={
                    <PrivateRoute>
                        <Dashboard />
                    </PrivateRoute>
                }
            />
        </Routes>
    );
}

export default App;