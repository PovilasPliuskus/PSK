import React from 'react';
import { Container } from 'react-bootstrap';
import { BsArrowRepeat } from 'react-icons/bs';
import ScriptResources from '../../assets/resources/strings';
import './base.css';

interface LoadingProps {
  message?: string;
}

const Loading: React.FC<LoadingProps> = ({ message = ScriptResources.Loading }) => {
  return (
    <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: '80vh' }}>
      <div className="text-center p-5 rounded shadow-lg" style={{ 
        maxWidth: '500px', 
        background: 'linear-gradient(145deg, #ffffff, #f0f0f0)',
        border: '1px solid rgba(0,0,0,0.1)'
      }}>
        <div className="mb-4 d-flex justify-content-center">
          <div className="p-3 rounded-circle" style={{ 
            background: 'rgba(0, 123, 255, 0.1)', 
            width: '80px', 
            height: '80px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
          }}>
            <BsArrowRepeat size={40} color="#007bff" className="loading-spinner" />
          </div>
        </div>
        
        <h2 className="mb-3" style={{ color: '#343a40' }}>{ScriptResources.PleaseWait}</h2>
        
        <p className="text-muted mb-4">
          {message}
        </p>
        
        <div className="progress" style={{ height: '8px' }}>
          <div 
            className="progress-bar progress-bar-striped progress-bar-animated" 
            role="progressbar" 
            style={{ width: '100%', backgroundColor: '#007bff' }}
          />
        </div>
        
        <p className="mt-4 small text-muted">
          {ScriptResources.LoadingTakeLonger}
        </p>
      </div>
    </Container>
  );
};

export default Loading;