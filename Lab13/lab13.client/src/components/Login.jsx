import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../AuthContext';

const Login = () => {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const { login } = useAuth();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        try {
            const response = await axios.post('/api/account/login', formData);
            login(response.data);
            const userProfile = response.data.userProfile || response.data;
            localStorage.setItem('userProfile', JSON.stringify(userProfile));
            navigate('/profile');
        } catch (error) {
            if (error.response) {
                if (error.response.status === 401) {
                    setError('Invalid email or password. Please try again.');
                } else if (error.response.data?.Error) {
                    setError(error.response.data.Error);
                } else {
                    setError('An error occurred. Please try again later.');
                }
            } else if (error.request) {
                setError('No response from server. Please check your network connection.');
            } else {
                setError('An error occurred. Please try again.');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-5">
            <h2>Login</h2>
            {error && (
                <div className="alert alert-danger" role="alert">
                    {error}
                </div>
            )}

            <div className="form-group mb-3">
                <label htmlFor="email">Email</label>
                <input className="form-control"
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
            </div>
            <div className="form-group mb-3">
                <label htmlFor="password">Password</label>
                <input className="form-control"
                    type="password"
                    name="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />
            </div>
            <button className="btn btn-primary" type="submit">Login</button>
        </form>
    );
};

export default Login;
