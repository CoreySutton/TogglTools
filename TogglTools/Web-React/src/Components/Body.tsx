import React, {FC, useState} from 'react';
import { Container } from 'react-bootstrap';
import Authenticate from './Authenticate';
import { TogglApi } from '../Api/TogglApi';
import { Entry } from '../Models/Report';
import { Workspace } from '../Models/Workspace';
import ReportForm from './ReportForm';
import ReportTable from './ReportTable';
import WorkspaceSelector from './WorkspaceSelector';

type Props = {
    setFullName: (fullName: string) => void;
}

const Body: FC<Props> = ({setFullName}: Props) => 
{   
    const [api] = useState(new TogglApi());
    const [authenticated, setAuthenticated] = useState(false);
    const [workspaces, setWorkspaces] = useState([] as Workspace[]); 
    const [selectedWorkspace, setSelectedWorkspace] = useState<Workspace>({} as Workspace);
    const [entries, setEntries] = useState([] as Entry[])

    const onAuthenticationChange = (authenticated: boolean) => 
    {
        setAuthenticated(authenticated);

        if (authenticated) {    
            setFullName(api.Me.fullname);
            setWorkspaces(api.Me.workspaces);
        }
        else 
        {
            setFullName('');
            setWorkspaces([] as Workspace[]);
            setSelectedWorkspace({} as Workspace);
            setEntries([] as Entry[])
        }
    }

    return (
        <Container fluid="xl">
            <Authenticate
                authenticated={ authenticated } 
                onAuthenticationChange={ onAuthenticationChange } 
                api= { api }/>

            {
                workspaces.length > 0 && 
                <WorkspaceSelector 
                    workspaces={workspaces} 
                    selectedWorkspaceId={selectedWorkspace?.id}
                    onWorkspaceSelected={setSelectedWorkspace} 
                />
            }

            {
                selectedWorkspace.id &&
                <ReportForm 
                    api={api} 
                    selectedWorkspace={selectedWorkspace}
                    setEntries={ setEntries }
                />
            }
            {
                entries.length > 0 &&
                <ReportTable entries={ entries } /> 
            }
        </Container>
    )
}

export default Body