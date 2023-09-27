import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';

function Header() {
    return (
      <header>
        <Navbar bg="light" data-bs-theme="light">
          <Container>
             <Col>
              <Navbar.Brand href="#home">DSML</Navbar.Brand>
              <Nav className="me-auto">
                <Nav.Link href="#home">Home</Nav.Link>
                <Nav.Link href="#features">Features</Nav.Link>
                <Nav.Link href="#pricing">Pricing</Nav.Link>
                <Nav.Link href="#pricing">Login</Nav.Link>
              </Nav>
            </Col>
            <Col xs={3} sm={4}>
              <Button>Log in with Dropbox</Button>
            </Col>
          </Container>
        </Navbar>
      </header>
    );
}

export default Header;