import { createContext, useContext, useState } from 'react';
import PropTypes from 'prop-types';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [userName, setUserName] = useState('');

    const login = (profile) => {
        setIsAuthenticated(true);
        setUserName(profile.userName);
    };

    const logout = () => {
        setIsAuthenticated(false);
        setUserName('');
        localStorage.removeItem('userProfile');
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, userName, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

AuthProvider.propTypes = {
    children: PropTypes.node.isRequired,
};

export const useAuth = () => useContext(AuthContext);
