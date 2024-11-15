import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../AuthContext';

const Register = () => {
    const [formData, setFormData] = useState({
        userName: '',
        fullName: '',
        phone: '',
        email: '',
        password: '',
        confirmPassword: '',
    });
    const [errors, setErrors] = useState({});
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.post('/api/account/register', formData);
            setErrors({});

            const loginData = {
                email: formData.email,
                password: formData.password,
            };
            const response = await axios.post('/api/account/login', loginData);
            login(response.data);
            const userProfile = response.data.userProfile || response.data;
            localStorage.setItem('userProfile', JSON.stringify(userProfile));

            navigate('/profile');
        } catch (error) {
            if (error.response && error.response.data) {
                console.error('Error response:', error.response.data);
                if (error.response.data.errors) {
                    setErrors(error.response.data.errors);
                } else {
                    alert('Error: ' + JSON.stringify(error.response.data));
                }
            } else {
                alert('Registration failed');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-5">
            <h2>Register</h2>
            <div className="form-group mb-3">
                <label htmlFor="userName" className="form-label">Username</label>
                <input
                    className={`form-control ${errors.UserName ? 'is-invalid' : ''}`}
                    type="text"
                    name="userName"
                    placeholder="Username"
                    value={formData.userName}
                    onChange={handleChange}
                    required
                />
                {errors.UserName && <div className="invalid-feedback">{errors.UserName}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="fullName" className="form-label">Full Name</label>
                <input
                    className={`form-control ${errors.FullName ? 'is-invalid' : ''}`}
                    type="text"
                    name="fullName"
                    placeholder="Full Name"
                    value={formData.fullName}
                    onChange={handleChange}
                    required
                />
                {errors.FullName && <div className="invalid-feedback">{errors.FullName}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="phone" className="form-label">Phone</label>
                <input
                    className={`form-control ${errors.Phone ? 'is-invalid' : ''}`}
                    type="tel"
                    name="phone"
                    placeholder="Phone"
                    value={formData.phone}
                    onChange={handleChange}
                    required
                />
                {errors.Phone && <div className="invalid-feedback">{errors.Phone}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="email" className="form-label">Email</label>
                <input
                    className={`form-control ${errors.Email ? 'is-invalid' : ''}`}
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
                {errors.Email && <div className="invalid-feedback">{errors.Email}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="password" className="form-label">Password</label>
                <input
                    className={`form-control ${errors.Password ? 'is-invalid' : ''}`}
                    type="password"
                    name="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />
                {errors.Password && <div className="invalid-feedback">{errors.Password}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
                <input
                    className={`form-control ${errors.ConfirmPassword ? 'is-invalid' : ''}`}
                    type="password"
                    name="confirmPassword"
                    placeholder="Confirm Password"
                    value={formData.confirmPassword}
                    onChange={handleChange}
                    required
                />
                {errors.ConfirmPassword && <div className="invalid-feedback">{errors.ConfirmPassword}</div>}
            </div>
            <button className="btn btn-primary" type="submit">Register</button>
        </form>
    );
};

export default Register;
