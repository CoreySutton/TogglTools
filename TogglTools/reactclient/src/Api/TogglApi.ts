import Config from "../Config";
import { Me } from "../Models/UserInformation";
import { Workspace } from "../Models/Workspace";
import dayjs from 'dayjs';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import { Entry } from "../Models/Report";

dayjs.extend(customParseFormat);

export class TogglApi 
{
    public Email: string = '';
    public Password: string = '';
    public ApiToken: string = '';

    public Me = {} as Me;
    public Workspaces = new Array<Workspace>();

    public async Login(email: string, password: string, apiToken: string) 
    {
        this.Email = email;
        this.Password = password;
        this.ApiToken = apiToken;

        const me = await this.Request<Me>(Config.Endpoints.Toggl.Me, "GET");

        this.Me = me;
    }

    public Logout() {
        this.Me = {} as Me;
    }

    public async Request<T > (endpoint: string, method: string): Promise<T>
    {
        const resource = `${Config.Endpoints.Toggl.Domain}${endpoint}`;
        const options: RequestInit = {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Basic ${btoa(`${this.ApiToken ?? this.Email}:${this.ApiToken ? 'api_token' : this.Password}`)}`
            }
        }
        
        const response = await fetch(resource, options);
        const {data, errors}: ResponseJson<T> = await response.json();

        if (response.ok) {
            return data as T;
        }
        else {
            const error = new Error(errors?.map(e => e.message).join('\n') ?? 'unknown')
            return Promise.reject(error)
        }
    }

    public async GetWorkspaces(): Promise<Workspace[]>
    {
        return this.Request<Workspace[]>(Config.Endpoints.Toggl.Workspace, "GET");
    }

    public async GetEntries(
        workspaceId: string, 
        since: string,
        until: string) :Promise<Entry[]> 
    {
        const resource = 
            `${Config.Endpoints.Toggl.Details}` 
            + `?workspace_id=${workspaceId}`
            + `&since=${dayjs(since, 'DD/MM/YYYY').format('YYYY-MM-DD')}`
            + `&until=${dayjs(until, 'DD/MM/YYYY').format('YYYY-MM-DD')}` 
            + `&user_agent=${Config.UserAgent}`;
            
        return this.Request<Entry[]>(resource, "GET");
    }

    public static SortEntriesPerClientProjectPerDay(entries: Entry[]) 
    {
        let clientProjectsPerDays = new Array<ClientProjectsPerDay>();
        entries.forEach(e => 
        {
            // Find or create day
            const formattedDate = dayjs(e.start).format('YYYY-MM-DD');
            let day = clientProjectsPerDays.find(d => d.Date === formattedDate);
            if (!day) {
                clientProjectsPerDays.push({
                    Date: formattedDate,
                    ClientProjects: new Array<ClientProject>()
                });

                day = clientProjectsPerDays.find(d => d.Date === formattedDate);
            }

            if (!day) throw Error("This shouldn't happen but day is null");

            // Find or create client
            let clientProject = day.ClientProjects.find(c => c.Client === e.client && c.Project === e.project);
            if (!clientProject) {
                day?.ClientProjects.push({
                    Client: e.client,
                    Project: e.project,
                    TotalHours: 0,
                    Entries: new Array<Entry>()
                });
                clientProject = day.ClientProjects.find(c => c.Client === e.client && c.Project === e.project);
            }
            
            if (!clientProject) throw Error("This shouldn't happen but client project is null");

            // Add to report
            clientProject.Entries.push(e);
            clientProject.TotalHours += (e.dur/3600000);
        });

        return clientProjectsPerDays;
    }
}

export interface ClientProjectsPerDay 
{
    Date: string;
    ClientProjects: ClientProject[]
}

export interface ClientProject {
    Client: string;
    Project: string;
    TotalHours: number;
    Entries: Entry[]
}

export interface ResponseJson<T>
{
    data?: T;
    errors?: Array<{message: string}>;
}