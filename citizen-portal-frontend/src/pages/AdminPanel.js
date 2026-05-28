import { useState, useEffect } from 'react';
import Navbar from '../components/Navbar';
import api from '../services/api';
import './AdminPanel.css';

const AdminPanel = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await api.get('/user');
        setUsers(response.data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };
    fetchUsers();
  }, []);

  const toggleActive = async (user) => {
    try {
      await api.put(`/user/${user.id}`, { name: user.name, isActive: !user.isActive });
      setUsers(users.map((u) => u.id === user.id ? { ...u, isActive: !u.isActive } : u));
    } catch (error) {
      console.error(error);
    }
  };

  const deleteUser = async (id) => {
    if (!window.confirm('Er du sikker på, at du vil slette denne bruger?')) return;
    try {
      await api.delete(`/user/${id}`);
      setUsers(users.filter((u) => u.id !== id));
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <Navbar />
      <div className="container">
        <div className="page-header">
          <h2>Brugeradministration</h2>
        </div>
        {loading ? (
          <p>Indlæser...</p>
        ) : (
          <div className="card">
            <table>
              <thead>
                <tr>
                  <th>Navn</th>
                  <th>Email</th>
                  <th>Rolle</th>
                  <th>Status</th>
                  <th>Handlinger</th>
                </tr>
              </thead>
              <tbody>
                {users.map((u) => (
                  <tr key={u.id}>
                    <td>{u.name}</td>
                    <td>{u.email}</td>
                    <td>{u.role}</td>
                    <td>
                      <span className={u.isActive ? 'status-godkendt' : 'status-afvist'}>
                        {u.isActive ? 'Aktiv' : 'Inaktiv'}
                      </span>
                    </td>
                    <td>
                      <div style={{ display: 'flex', gap: '8px' }}>
                        <button
                          className={`btn ${u.isActive ? 'btn-danger' : 'btn-success'}`}
                          onClick={() => toggleActive(u)}
                        >
                          {u.isActive ? 'Deaktiver' : 'Aktiver'}
                        </button>
                        <button
                          className="btn btn-danger"
                          onClick={() => deleteUser(u.id)}
                        >
                          Slet
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
};

export default AdminPanel;