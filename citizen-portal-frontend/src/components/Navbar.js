import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <nav className="navbar">
      <div className="navbar-brand">
        <h1>CitizenPortal</h1>
        <span>Dansk borgertjeneste</span>
      </div>
      <div className="navbar-user">
        <span>{user?.name}</span>
        <span className="navbar-role">{user?.role}</span>
        <button onClick={handleLogout}>Log ud</button>
      </div>
    </nav>
  );
};

export default Navbar;