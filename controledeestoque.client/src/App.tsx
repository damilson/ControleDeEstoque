// src/App.tsx
import React, { useState } from 'react';
import Login from './components/Login/Login';
import Dashboard from './components/Dashboard/Dashboard';
import './App.css';

function App() {
    // Estado para controlar qual componente exibir
    const [showDashboard, setShowDashboard] = useState(false);

    const toggleView = () => {
        setShowDashboard(!showDashboard);
    };

    return (
        <div className="App">
            {/* Botão para alternar a visualização - APENAS PARA DESENVOLVIMENTO */}
            <div style={{ padding: '10px', textAlign: 'center', backgroundColor: '#f0f0f0' }}>
                <button onClick={toggleView} style={{ padding: '8px 15px', fontSize: '16px' }}>
                    {showDashboard ? 'Ver Tela de Login' : 'Ver Tela do Dashboard (Dev)'}
                </button>
                <p style={{fontSize: '12px', color: '#777', marginTop: '5px'}}>
                    (Este botão é apenas para facilitar o desenvolvimento)
                </p>
            </div>

            <hr />

            {/* Renderiza condicionalmente o Login ou o Dashboard */}
            {showDashboard ? <Dashboard /> : <Login />}
            
        </div>
    );
}

export default App;