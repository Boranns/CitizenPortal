import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { login as loginService } from '../services/authService';
import './Login.css';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const data = await loginService({ email, password });
      login({ name: data.name, email: data.email, role: data.role }, data.token, data.refreshToken);
      if (data.role === 'Borger') navigate('/mine-ansogninger');
      else if (data.role === 'Sagsbehandler') navigate('/ansogninger');
      else if (data.role === 'Admin') navigate('/admin');
    } catch {
      setError('Ugyldig email eller adgangskode');
    }
  };

  return (
    <div className="login-page">
      <div className="login-card">
        <div className="login-logo">
          <h1>CitizenPortal</h1>
          <p>Dansk borgertjeneste</p>
        </div>
        {error && <p className="error">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Email</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="din@email.dk"
            />
          </div>
          <div className="form-group">
            <label>Adgangskode</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="••••••••"
            />
          </div>
          <button type="submit" className="btn btn-primary" style={{ width: '100%' }}>
            Log ind
          </button>
        </form>
        <div className="login-footer">
          <p>Har du ikke en konto? <a href="/register">Opret konto</a></p>
        </div>
      </div>
    </div>
  );
};

export default Login;