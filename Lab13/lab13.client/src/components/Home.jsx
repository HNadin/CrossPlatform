import { useNavigate } from 'react-router-dom';

const Home = () => {
    const navigate = useNavigate();

    const handleLogin = () => {
        navigate('/login');
    };

    return (
        <div className="container text-center mt-5">
            <h1 className="display-4">👋 Ласкаво просимо до веб-застосунку!</h1>
            <p className="lead mt-4">
                Цей застосунок пропонує вам наступні можливості:
            </p>
            <div className="features-list">
                <ul className="list-group list-group-flush">
                    <li className="list-group-item">
                        <i className="fas fa-user-plus"></i> Реєстрація та авторизація для доступу до функцій.
                    </li>
                    <li className="list-group-item">
                        <i className="fas fa-tasks"></i> Виконання практичних робіт через підпрограми.
                    </li>
                    <li className="list-group-item">
                        <i className="fas fa-user-circle"></i> Перегляд профілю особистих даних.
                    </li>
                </ul>
            </div>
            <p className="mt-4">
                <i>Для доступу до підпрограм вам потрібно <strong>увійти</strong> в систему.</i>
            </p>
            <button onClick={handleLogin} className="btn btn-primary mt-3">
                Увійти зараз
            </button>
        </div>
    );
};

export default Home;
