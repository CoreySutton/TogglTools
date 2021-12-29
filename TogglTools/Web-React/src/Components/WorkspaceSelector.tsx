import React, { FC } from 'react';
import { Workspace } from '../Models/Workspace';
import Button from 'react-bootstrap/Button';
import { Col, Row } from 'react-bootstrap';

type Props = {
    workspaces: Workspace[];
    onWorkspaceSelected: (workspace: Workspace) => void ;
    selectedWorkspaceId?: number;
}

const WorkspaceSelector: FC<Props> = ({workspaces, onWorkspaceSelected, selectedWorkspaceId}: Props) => 
{
    const fakeWorkspace: Workspace = {
        id: 999,
        name: "Fake Workspace 1",
        profile: 1,
        premium: false,
        admin: false,
        default_hourly_rate: 1,
        default_currency: "",
        only_admins_may_create_projects: false,
        only_admins_see_billable_rates: false,
        only_admins_see_team_dashboard: false,
        projects_billable_by_default: false,
        rounding: 1,
        rounding_minutes: 1,
        api_token: "",
        at: "",
        ical_enabled: false
    }

    return (
        <Row>
            {
                workspaces.map(workspace => {
                    return (<Col key={workspace.id} >
                        <Button 
                            variant={ selectedWorkspaceId === workspace.id ? "primary" : "outline-primary" }
                            onClick={e => onWorkspaceSelected(workspace)}
                        >
                            {workspace.name}
                        </Button>
                    </Col>)
                })
            }
            <Col key="999">
                <Button 
                    variant={ selectedWorkspaceId === fakeWorkspace.id ? "primary" : "outline-primary" }
                    onClick={e => onWorkspaceSelected(fakeWorkspace)}
                >
                    {fakeWorkspace.name}
                </Button>
            </Col>
        </Row>
    );
}

export default WorkspaceSelector;