import { Link, useNavigate } from 'react-router-dom';
import './Header.css';
import { useAuth } from '../AuthContext';

const Header = () => {
    const { isAuthenticated, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/');
    };

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container-fluid">
                    <Link className="navbar-brand" to="/">Lab13</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <Link className="nav-link text-dark" to="/">Home</Link>
                            </li>
                            {isAuthenticated ? (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/profile">Профіль</Link>
                                    </li>
                                    <li className="nav-item">
                                        <button className="nav-link btn btn-link" onClick={handleLogout}>Вийти</button>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/lab1">Підпрограма 1</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/lab2">Підпрограма 2</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/lab3">Підпрограма 3</Link>
                                    </li>
                                </>
                            ) : (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/login">Увійти</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/register">Зареєструватися</Link>
                                    </li>
                                </>
                            )}
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
};

export default Header;
