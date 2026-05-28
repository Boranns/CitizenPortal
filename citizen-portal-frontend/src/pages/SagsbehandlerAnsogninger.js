import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../components/Navbar';
import api from '../services/api';
import './SagsbehandlerAnsogninger.css';

const SagsbehandlerAnsogninger = () => {
  const [ansogninger, setAnsogninger] = useState([]);
  const [filter, setFilter] = useState('alle');
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAnsogninger = async () => {
      try {
        const response = await api.get('/applications');
        setAnsogninger(response.data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };
    fetchAnsogninger();
  }, []);

  const filteredAnsogninger = ansogninger.filter((a) => {
    if (filter === 'alle') return true;
    return a.status.toLowerCase() === filter;
  });

  return (
    <div>
      <Navbar />
      <div className="container">
        <div className="page-header">
          <h2>Alle ansøgninger</h2>
        </div>
        <div className="filter-bar">
          <button className={`filter-btn ${filter === 'alle' ? 'active' : ''}`} onClick={() => setFilter('alle')}>Alle</button>
          <button className={`filter-btn ${filter === 'afventer' ? 'active' : ''}`} onClick={() => setFilter('afventer')}>Afventer</button>
          <button className={`filter-btn ${filter === 'godkendt' ? 'active' : ''}`} onClick={() => setFilter('godkendt')}>Godkendt</button>
          <button className={`filter-btn ${filter === 'afvist' ? 'active' : ''}`} onClick={() => setFilter('afvist')}>Afvist</button>
        </div>
        {loading ? (
          <p>Indlæser...</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>Titel</th>
                <th>Borger</th>
                <th>Status</th>
                <th>Dato</th>
                <th>Handling</th>
              </tr>
            </thead>
            <tbody>
              {filteredAnsogninger.map((a) => (
                <tr key={a.id}>
                  <td>{a.title}</td>
                  <td>{a.userName}</td>
                  <td><span className={`status-${a.status.toLowerCase()}`}>{a.status}</span></td>
                  <td>{new Date(a.createdAt).toLocaleDateString('da-DK')}</td>
                  <td>
                    <button className="btn btn-secondary" onClick={() => navigate(`/sagsbehandler/ansogning/${a.id}`)}>
                      Se detaljer
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default SagsbehandlerAnsogninger;