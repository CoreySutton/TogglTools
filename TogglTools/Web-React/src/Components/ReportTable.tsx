import React, {FC} from 'react';
import { Row, Tab, Table, Tabs } from 'react-bootstrap';
import { TogglApi } from '../Api/TogglApi';
import { Entry } from '../Models/Report';
import ReportTableRow from './ReportTableRow';

type Props = {
    entries: Entry[];
}

const ReportTable: FC<Props> = ({entries}: Props) => 
{
    const clientProjectsPerDays = TogglApi.SortEntriesPerClientProjectPerDay(entries);

    return (
        <Row>
            <Tabs defaultActiveKey={clientProjectsPerDays[0].Date}>
            {
                clientProjectsPerDays.map(day => 
                {
                    return (
                        <Tab eventKey={day.Date} title={day.Date} key={day.Date}>
                            <Table striped bordered hover>
                                <thead>
                                    <tr>
                                        <th>Day</th>
                                        <th>Client</th>
                                        <th>Project</th>
                                        <th>Notes</th>
                                        <th>Total Hours</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    { 
                                        day.ClientProjects.map(clientProject =>
                                            <ReportTableRow 
                                                key={day.Date + clientProject.Client + clientProject.Project} 
                                                clientProject={clientProject}
                                                date={day.Date}
                                            />
                                        )
                                    }
                                </tbody>
                            </Table>
                        </Tab>
                    );
                })
            }
            </Tabs>
        </Row>
    )
}

export default ReportTable