import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Container, Navbar, Nav } from 'react-bootstrap';
import DemoForm from './components/domain/Demo';

// Home Page Component
const HomePage: React.FC = () => {
  return (
    <Container className="mt-5 text-center">
      <h1>Welcome to Our Application</h1>
      <p>Navigate to the Demo Form to get started!</p>
    </Container>
  );
};

// Not Found Page Component
const NotFoundPage: React.FC = () => {
  return (
    <Container className="mt-5 text-center">
      <h1>404 - Page Not Found</h1>
      <p>The page you are looking for does not exist.</p>
      <Link to="/" className="btn btn-primary">
        Go to Home
      </Link>
    </Container>
  );
};

function App() {
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
                <Nav.Link as={Link} to="/demo-form">
                  Demo Form
                </Nav.Link>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>

        {/* Routes */}
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/demo-form" element={<DemoForm />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;