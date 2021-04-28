import React, { FC, useEffect, useState } from 'react';
import { Form, Button, ButtonGroup, InputGroup, Row, Col } from 'react-bootstrap';
import { TogglApi } from '../Api/TogglApi';

type Props = {
    onAuthenticationChange: (authenticated: boolean) => void;
    authenticated: boolean;
    api: TogglApi;
}

const Authenticate: FC<Props> = ({onAuthenticationChange, authenticated, api}: Props) =>
{
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [apiToken, setApiToken] = useState('');

    useEffect(
        () => {
            if (!authenticated) {
                (document.getElementById("email") as HTMLInputElement).value = '';
                (document.getElementById("password") as HTMLInputElement).value = '';
                (document.getElementById("api-token") as HTMLInputElement).value = '';
            }
        }, 
        [authenticated]
    );

    const Reset = () => {
        onAuthenticationChange(false);
    }

    const OnSubmit = async(event: React.FormEvent<HTMLFormElement | HTMLInputElement>) => 
    {
        event.preventDefault();

        if (!apiToken && (!email || !password)) return;

        try {
            await api.Login(email, password, apiToken);
            onAuthenticationChange(true);
        }
        catch(error) {
            console.error("An error occurred during login");
            console.error(error);
            onAuthenticationChange(false);
        }
    }

    return (
        <Row><Col>
            <Form onSubmit={OnSubmit}>
                <Form.Row>

                    <Form.Group controlId="email">
                        <Form.Label srOnly>Email</Form.Label>
                        <InputGroup className="mb-2 mr-sm-2">
                            <InputGroup.Prepend>
                            <InputGroup.Text>Email</InputGroup.Text>
                            </InputGroup.Prepend>
                            <Form.Control 
                                type="email" 
                                placeholder="someone@example.com" 
                                readOnly={authenticated} 
                                onChange={e => setEmail(e.target.value)}
                            />
                        </InputGroup>
                        {/* <Form.Text muted>
                            We don't store this value.
                        </Form.Text> */}
                    </Form.Group>

                    
                    <Form.Group controlId="password">
                        <Form.Label srOnly>Password</Form.Label>
                        <InputGroup className="mb-2 mr-sm-2">
                            <InputGroup.Prepend>
                            <InputGroup.Text>Password</InputGroup.Text>
                            </InputGroup.Prepend>
                            <Form.Control 
                                type="password" 
                                placeholder="****************************" 
                                readOnly={authenticated} 
                                onChange={e => setPassword(e.target.value)}
                            />
                        </InputGroup>
                        {/* <Form.Text muted>
                            We don't store this value.
                        </Form.Text> */}
                    </Form.Group>
                    
                    <Form.Group controlId="api-token">
                        <Form.Label srOnly>API Token</Form.Label>
                        <InputGroup className="mb-2 mr-sm-2">
                            <InputGroup.Prepend>
                            <InputGroup.Text>API Token</InputGroup.Text>
                            </InputGroup.Prepend>
                            <Form.Control 
                                type="password" 
                                placeholder="****************************" 
                                readOnly={authenticated} 
                                onChange={e => setApiToken(e.target.value)}
                            />
                        </InputGroup>
                        {/* <Form.Text muted>
                            We don't store this value.
                        </Form.Text> */}
                    </Form.Group>

                    <ButtonGroup>
                        <Button 
                            variant="outline-primary" 
                            type="submit"
                            disabled={authenticated} 
                            onSubmit={OnSubmit}>
                            Authenticate
                        </Button>
                        <Button variant='outline-danger' onClick={ Reset }>Reset</Button>
                    </ButtonGroup>
                </Form.Row>
            </Form>
        </Col></Row>
    );
}

export default Authenticate;
