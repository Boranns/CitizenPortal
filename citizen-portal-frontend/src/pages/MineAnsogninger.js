import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../components/Navbar';
import api from '../services/api';
import './MineAnsogninger.css';

const MineAnsogninger = () => {
  const [ansogninger, setAnsogninger] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAnsogninger = async () => {
      try {
        const response = await api.get('/applications/my');
        setAnsogninger(response.data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };
    fetchAnsogninger();
  }, []);

  return (
    <div>
      <Navbar />
      <div className="container">
        <div className="page-header">
          <h2>Mine ansøgninger</h2>
          <button
            className="btn btn-primary"
            onClick={() => navigate('/ny-ansogning')}
          >
            Ny ansøgning
          </button>
        </div>
        {loading ? (
          <p>Indlæser...</p>
        ) : ansogninger.length === 0 ? (
          <div className="card">
            <p>Du har ingen ansøgninger endnu.</p>
          </div>
        ) : (
          ansogninger.map((a) => (
            <div key={a.id} className="card ansogning-card" onClick={() => navigate(`/ansogning/${a.id}`)}>
              <div className="ansogning-header">
                <h3>{a.title}</h3>
                <span className={`status-${a.status.toLowerCase()}`}>{a.status}</span>
              </div>
              <p>{a.description}</p>
              <small>{new Date(a.createdAt).toLocaleDateString('da-DK')}</small>
            </div>
          ))
        )}
      </div>
    </div>
  );
};

export default MineAnsogninger;