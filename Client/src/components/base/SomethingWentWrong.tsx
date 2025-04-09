import React from 'react';
import { Container } from 'react-bootstrap';
import { BsTools, BsArrowClockwise, BsHouseDoor } from 'react-icons/bs';
import ScriptResources from '../../assets/resources/strings';

interface SomethingWentWrongProps {
  onRetry: () => void;
}

const SomethingWentWrong: React.FC<SomethingWentWrongProps> = ({ onRetry }) => {
  const onGoHome = () => {
    window.location.href = 'http://localhost:5173';
};
  return (
    <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: '80vh' }}>
      <div className="text-center p-5 rounded shadow-lg" style={{ 
        maxWidth: '500px', 
        background: 'linear-gradient(145deg, #ffffff, #f0f0f0)',
        border: '1px solid rgba(0,0,0,0.1)'
      }}>
        <div className="mb-4 d-flex justify-content-center">
          <div className="p-3 rounded-circle" style={{ 
            background: 'rgba(220, 53, 69, 0.1)', 
            width: '80px', 
            height: '80px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
          }}>
            <BsTools size={40} color="#dc3545" style={{ transform: 'rotate(15deg)' }} />
          </div>
        </div>
        
        <h2 className="mb-3" style={{ color: '#343a40' }}>{ScriptResources.SomethingWentWrong}</h2>
        
        <p className="text-muted mb-4">
          {ScriptResources.SomethingWentWrongDescription}
        </p>
        
        <div className="d-flex justify-content-center gap-3">
          <button 
            onClick={onRetry}
            className="btn btn-danger px-4 py-2"
            style={{ 
              borderRadius: '50px',
              boxShadow: '0 4px 6px rgba(220, 53, 69, 0.2)',
              transition: 'all 0.3s ease'
            }}
          >
            <span className="me-2">{ScriptResources.SomethingWentWrongRetry}</span>
            <BsArrowClockwise />
          </button>
          
          <button 
            onClick={onGoHome}
            className="btn btn-outline-secondary px-4 py-2"
            style={{ 
              borderRadius: '50px',
              transition: 'all 0.3s ease'
            }}
          >
            <span className="me-2">{ScriptResources.SomethingWentWrongGoHome}</span>
            <BsHouseDoor />
          </button>
        </div>
        
        <p className="mt-4 small text-muted">
          {ScriptResources.SomethingWentWrongContactSupport}
        </p>
      </div>
    </Container>
  );
};

export default SomethingWentWrong;