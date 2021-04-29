import React, { useState } from 'react';
import './App.css';
import { Container, Navbar } from 'react-bootstrap';
import Body from './Components/Body';

function App() {
  
  const [fullName, setFullName] = useState('');

  return (
    <>
      <Container>

        <Navbar bg="dark" variant="dark" expand="lg">
          <Navbar.Brand>Toggl Reporting</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse className="justify-content-end">
            <Navbar.Text>
              { fullName ?  `Welcome ${fullName}` : 'Not authenticated' }
            </Navbar.Text>
          </Navbar.Collapse>
        </Navbar>

        <Body setFullName={setFullName}/>

      </Container>
    </>
  );
}

export default App;
