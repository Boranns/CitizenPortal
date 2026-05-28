import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import Login from './pages/Login';
import Register from './pages/Register';
import MineAnsogninger from './pages/MineAnsogninger';
import NyAnsogning from './pages/NyAnsogning';
import AnsogningDetaljer from './pages/AnsogningDetaljer';
import SagsbehandlerAnsogninger from './pages/SagsbehandlerAnsogninger';
import SagsbehandlerDetaljer from './pages/SagsbehandlerDetaljer';
import AdminPanel from './pages/AdminPanel';

const PrivateRoute = ({ children, roles }) => {
  const { user } = useAuth();
  if (!user) return <Navigate to="/login" />;
  if (roles && !roles.includes(user.role)) return <Navigate to="/login" />;
  return children;
};

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          
          <Route path="/mine-ansogninger" element={
            <PrivateRoute roles={['Borger']}>
              <MineAnsogninger />
            </PrivateRoute>
          } />
          <Route path="/ny-ansogning" element={
            <PrivateRoute roles={['Borger']}>
              <NyAnsogning />
            </PrivateRoute>
          } />
          <Route path="/ansogning/:id" element={
            <PrivateRoute roles={['Borger']}>
              <AnsogningDetaljer />
            </PrivateRoute>
          } />

          <Route path="/ansogninger" element={
            <PrivateRoute roles={['Sagsbehandler', 'Admin']}>
              <SagsbehandlerAnsogninger />
            </PrivateRoute>
          } />
          <Route path="/sagsbehandler/ansogning/:id" element={
            <PrivateRoute roles={['Sagsbehandler', 'Admin']}>
              <SagsbehandlerDetaljer />
            </PrivateRoute>
          } />

          <Route path="/admin" element={
            <PrivateRoute roles={['Admin']}>
              <AdminPanel />
            </PrivateRoute>
          } />

          <Route path="*" element={<Navigate to="/login" />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;