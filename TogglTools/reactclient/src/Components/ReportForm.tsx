import React, { FC, useState } from 'react';
import { Button, Col, Form, FormControl, InputGroup, Row } from 'react-bootstrap';
import { TogglApi } from '../Api/TogglApi';
import { Entry } from '../Models/Report';
import { Workspace } from '../Models/Workspace';

type Props = {
    api: TogglApi;
    selectedWorkspace: Workspace;
    setEntries: (entries: Entry[]) => void;
}

const ReportForm: FC<Props> = ({api, selectedWorkspace, setEntries}: Props) => 
{
    const [since, setSince] = useState('19/04/2021');
    const [until, setUntil] = useState('23/04/2021');

    const OnSubmit = async(event: React.FormEvent<HTMLFormElement | HTMLInputElement>) => {
        event.preventDefault();

        try {
            const report = await api.GetEntries(
                selectedWorkspace.id.toString(), 
                since,
                until);
            setEntries(report);
        }
        catch(error) {
            console.error("An error occurred when generating report");
            console.error(error);
            
            setEntries({} as Entry[]);
        }
    }

    return (
        <Row><Col>
            <Form onSubmit={OnSubmit}>
                <Form.Row>
                    <Form.Label htmlFor="start-date" srOnly>
                        Start Date
                    </Form.Label>
                    <InputGroup className="mb-2 mr-sm-2">
                        <InputGroup.Prepend>
                        <InputGroup.Text>Start Date</InputGroup.Text>
                        </InputGroup.Prepend>
                        <FormControl 
                        id="start-date" 
                        placeholder="01/01/2021" 
                        onChange={e => setSince(e.target.value)}
                        value='01/01/2021'/>
                    </InputGroup>

                    <Form.Label htmlFor="end-date" srOnly>
                        End Date
                    </Form.Label>
                    <InputGroup className="mb-2 mr-sm-2">
                        <InputGroup.Prepend>
                        <InputGroup.Text>End Date</InputGroup.Text>
                        </InputGroup.Prepend>
                        <FormControl 
                            id="end-date" 
                            placeholder="07/01/2021"  
                            onChange={e => setUntil(e.target.value)}
                            value='07/01/2021'/>
                    </InputGroup>

                    <Button 
                        variant="outline-primary" 
                        type="submit"
                        onSubmit={OnSubmit} 
                    >
                        Run Report
                    </Button>
                </Form.Row>
            </Form>
        </Col></Row>
    );
}

export default ReportForm;