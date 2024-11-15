import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Profile.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faIdBadge, faUser, faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons';
import { useAuth } from '../AuthContext';

const Profile = () => {
    const [profile, setProfile] = useState(null);
    const navigate = useNavigate(); 
    const { logout } = useAuth();

    useEffect(() => {
        const storedProfile = localStorage.getItem('userProfile');
        if (storedProfile) {
            setProfile(JSON.parse(storedProfile));
        } else {
            navigate('/login');
        }
    }, [navigate]);

    const handleLogout = async () => {
        try {
            await axios.post('/api/account/logout');
            logout();
            navigate('/');
        } catch (error) {
            console.error('Logout error:', error);
            alert('Logout failed: ' + (error.response?.data?.Error || error.message));
        }
    };

    if (!profile) {
        return <p>Loading profile...</p>;
    }

    return (
        <div className="profile-container container mt-5">
            <div className="profile-header d-flex align-items-center mb-4 border-bottom pb-3">
                {profile.profileImage && (
                    <img
                        src={profile.profileImage}
                        alt="Profile Image"
                        className="img-rounded img-responsive me-3"
                        style={{ width: '100px', height: '100px', borderRadius: '50%' }}
                    />
                )}
                <h2>Profile</h2>
            </div>

            <div className="profile-info">
                <p>
                    <FontAwesomeIcon icon={faIdBadge} className="me-2 text-primary" />
                    <strong>Full Name:</strong> {profile.fullName}
                </p>
                <p>
                    <FontAwesomeIcon icon={faUser} className="me-2 text-primary" />
                    <strong>Username:</strong> {profile.userName}
                </p>
                <p>
                    <FontAwesomeIcon icon={faEnvelope} className="me-2 text-primary" />
                    <strong>Email:</strong> {profile.email}
                </p>
                <p>
                    <FontAwesomeIcon icon={faPhone} className="me-2 text-primary" />
                    <strong>Phone:</strong> {profile.phone}
                </p>
            </div>

            <button onClick={handleLogout} className="btn btn-primary btn-logout mt-4">
                Logout
            </button>
        </div>
    );
};

export default Profile;
