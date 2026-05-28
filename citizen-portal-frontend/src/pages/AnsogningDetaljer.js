import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Navbar from '../components/Navbar';
import api from '../services/api';
import './AnsogningDetaljer.css';

const AnsogningDetaljer = () => {
  const { id } = useParams();
  const [ansogning, setAnsogning] = useState(null);
  const [kommentarer, setKommentarer] = useState([]);
  const [nyKommentar, setNyKommentar] = useState('');
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [ansRes, komRes] = await Promise.all([
          api.get(`/applications/${id}`),
          api.get(`/applications/${id}/comments`)
        ]);
        setAnsogning(ansRes.data);
        setKommentarer(komRes.data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [id]);

  const sendKommentar = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post(`/applications/${id}/comments`, { text: nyKommentar });
      setKommentarer([...kommentarer, response.data]);
      setNyKommentar('');
    } catch (error) {
      console.error(error);
    }
  };

  if (loading) return <div><Navbar /><div className="container"><p>Indlæser...</p></div></div>;
  if (!ansogning) return <div><Navbar /><div className="container"><p>Ansøgning ikke fundet</p></div></div>;

  return (
    <div>
      <Navbar />
      <div className="container">
        <button className="btn btn-secondary back-btn" onClick={() => navigate(-1)}>
          ← Tilbage
        </button>
        <div className="card">
          <div className="detaljer-header">
            <h2>{ansogning.title}</h2>
            <span className={`status-${ansogning.status.toLowerCase()}`}>{ansogning.status}</span>
          </div>
          <p className="detaljer-desc">{ansogning.description}</p>
          <small>Oprettet: {new Date(ansogning.createdAt).toLocaleDateString('da-DK')}</small>
        </div>

        <div className="card">
          <h3>Kommentarer</h3>
          {kommentarer.length === 0 ? (
            <p>Ingen kommentarer endnu.</p>
          ) : (
            kommentarer.map((k) => (
              <div key={k.id} className="kommentar">
                <strong>{k.userName}</strong>
                <p>{k.text}</p>
                <small>{new Date(k.createdAt).toLocaleDateString('da-DK')}</small>
              </div>
            ))
          )}
          <form onSubmit={sendKommentar} className="kommentar-form">
            <input
              type="text"
              value={nyKommentar}
              onChange={(e) => setNyKommentar(e.target.value)}
              placeholder="Skriv en kommentar..."
            />
            <button type="submit" className="btn btn-primary">Send</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default AnsogningDetaljer;