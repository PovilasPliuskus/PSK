import React from 'react';
import { useKeycloak } from '@react-keycloak/web';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Container, Navbar, Nav, Button } from 'react-bootstrap';
import DemoForm from './components/domain/Demo';


import {TaskBoardViewPage} from './components/domain/TaskBoard/TaskBoardViewPage';
import { axiosInstance, useAxiosInterceptor } from './utils/axiosInstance';
import { ErrorProvider } from './components/base/ErrorContext';
import GlobalAlert from './components/base/GlobalAlert';
import ScriptResources from './assets/resources/strings';
import Workspaces from './components/domain/Workspace/Workspaces';
import WorkspaceUsers from './components/domain/WorkspaceUsers/WorkspaceUsers';


// Home Page Component
const HomePage: React.FC = () => {
  return (
    <Container className="mt-5 text-center">
      <h1>{ScriptResources.IndexHeader}</h1>
      <p>{ScriptResources.IndexParagraph}</p>
    </Container>
  );
};

// Not Found Page Component
const NotFoundPage: React.FC = () => {
  return (
    <Container className="mt-5 text-center">
      <h1>{ScriptResources.Page404}</h1>
      <p>{ScriptResources.PageDoNotExist}</p>
      <Link to="/" className="btn btn-primary">
        {ScriptResources.PageGoToHome}
      </Link>
    </Container>
  );
};

function App() {

  return (
    <ErrorProvider>
      <InnerApp />
    </ErrorProvider>
  );
}

function InnerApp() {
  const { keycloak } = useKeycloak();
  useAxiosInterceptor();

  const fetchProtectedData = async () => {
    try {
      const response = await axiosInstance.get('/protected-endpoint');
      console.log('Protected data:', response.data);
    } catch (error) {
      console.error('Error fetching protected data:', error);
    }
  };

  const fetchErrorData = async () => {
    try {
      const response = await axiosInstance.get('/error');
      console.log('Error data:', response.data);
    } catch (error) {
      console.error('Error fetching error data:', error);
    }
  };

  return (
    <Router>
      <div className="App">
        {/* Navigation */}
        <Navbar bg="light" expand="lg">
          <Container>
            <Navbar.Brand as={Link} to="/">
              My App
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
              <Nav className="me-auto">
                <Nav.Link as={Link} to="/">
                  Home
                </Nav.Link>
                <Nav.Link as={Link} to="/workspaces">
                  Workspaces
                </Nav.Link>
              </Nav>
              <Nav className="m-2 flex items-center gap-4">
                {!keycloak.authenticated ? (
                  <Button onClick={() => keycloak.login()}>Sign In</Button>
                ) : (
                  <>
                    <p className="text-sm text-gray-700">Hello, {keycloak.tokenParsed?.name}</p>
                    <Button onClick={() => keycloak.logout()}>
                      Sign Out
                    </Button>
                  </>
                )}
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
        <GlobalAlert />

        {/* Routes */}
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/workspaces" element={<Workspaces />} />
          <Route path="/demo-form" element={<DemoForm />} />
          <Route path="/task-page/:id" element={<TaskBoardViewPage />} />
          <Route path="/workspace/users/:id/:name" element={<WorkspaceUsers />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App;