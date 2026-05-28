import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { register as registerService } from '../services/authService';
import './Register.css';

const Register = () => {
  const [form, setForm] = useState({ name: '', email: '', password: '', role: 'Borger' });
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await registerService(form);
      navigate('/login');
    } catch {
      setError('Registrering mislykkedes');
    }
  };

  return (
    <div className="register-page">
      <div className="register-card">
        <div className="register-logo">
          <h1>CitizenPortal</h1>
          <p>Opret en ny konto</p>
        </div>
        {error && <p className="error">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Navn</label>
            <input
              type="text"
              value={form.name}
              onChange={(e) => setForm({ ...form, name: e.target.value })}
              placeholder="Dit fulde navn"
            />
          </div>
          <div className="form-group">
            <label>Email</label>
            <input
              type="email"
              value={form.email}
              onChange={(e) => setForm({ ...form, email: e.target.value })}
              placeholder="din@email.dk"
            />
          </div>
          <div className="form-group">
            <label>Adgangskode</label>
            <input
              type="password"
              value={form.password}
              onChange={(e) => setForm({ ...form, password: e.target.value })}
              placeholder="••••••••"
            />
          </div>
          <button type="submit" className="btn btn-primary" style={{ width: '100%' }}>
            Opret konto
          </button>
        </form>
        <div className="register-footer">
          <p>Har du allerede en konto? <a href="/login">Log ind</a></p>
        </div>
      </div>
    </div>
  );
};

export default Register;