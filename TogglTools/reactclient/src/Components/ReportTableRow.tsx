import React, {FC} from 'react';
import { ClientProject } from '../Api/TogglApi';

type Props = {
    clientProject: ClientProject;
    date: string;
}

const ReportTableRow: FC<Props> = ({clientProject, date}: Props) => 
{
    return (
        <tr>
            <td>{ date }</td>
            <td>{ clientProject.Client }</td>
            <td>{ clientProject.Project }</td>
            <td>
                <ul>
                    { clientProject.Entries.map(entry => <li key={entry.id}>{entry.description}</li>) }
                </ul>
            </td>
            <td>{ clientProject.TotalHours }</td>
        </tr>
    )
}

export default ReportTableRow