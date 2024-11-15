import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { useState } from 'react';
import Home from './components/Home';
import Register from './components/Register';
import Login from './components/Login';
import Profile from './components/Profile';
import LabPage from './components/LabPage';
import Head from './components/Head';
import Header from './components/Header';

function App() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [userName, setUserName] = useState('');

    return (
        <Router>
            <Head />
            <Header isAuthenticated={isAuthenticated} userName={userName} />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/register" element={<Register />} />
                <Route path="/login" element={<Login setIsAuthenticated={setIsAuthenticated} setUserName={setUserName} />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/lab1" element={<LabPage labNumber="1" />} />
                <Route path="/lab2" element={<LabPage labNumber="2" />} />
                <Route path="/lab3" element={<LabPage labNumber="3" />} />
            </Routes>
        </Router>
    );
}

export default App;
