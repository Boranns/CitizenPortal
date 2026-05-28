import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../components/Navbar';
import api from '../services/api';
import './NyAnsogning.css';

const NyAnsogning = () => {
  const [form, setForm] = useState({ title: '', description: '' });
  const [file, setFile] = useState(null);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post('/applications', form);
      
      if (file) {
        const formData = new FormData();
        formData.append('file', file);
        await api.post(`/applications/${response.data.id}/documents`, formData, {
          headers: { 'Content-Type': 'multipart/form-data' }
        });
      }

      navigate('/mine-ansogninger');
    } catch {
      setError('Der opstod en fejl. Prøv igen.');
    }
  };

  return (
    <div>
      <Navbar />
      <div className="container">
        <div className="page-header">
          <h2>Ny ansøgning</h2>
        </div>
        <div className="card">
          {error && <p className="error">{error}</p>}
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label>Titel</label>
              <input
                type="text"
                value={form.title}
                onChange={(e) => setForm({ ...form, title: e.target.value })}
                placeholder="Ansøgningens titel"
              />
            </div>
            <div className="form-group">
              <label>Beskrivelse</label>
              <textarea
                value={form.description}
                onChange={(e) => setForm({ ...form, description: e.target.value })}
                placeholder="Beskriv din ansøgning..."
                rows={5}
              />
            </div>
            <div className="form-group">
              <label>Vedhæft dokument (valgfrit)</label>
              <input
                type="file"
                onChange={(e) => setFile(e.target.files[0])}
              />
            </div>
            <div className="form-actions">
              <button type="button" className="btn btn-secondary" onClick={() => navigate('/mine-ansogninger')}>
                Annuller
              </button>
              <button type="submit" className="btn btn-primary">
                Send ansøgning
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default NyAnsogning;