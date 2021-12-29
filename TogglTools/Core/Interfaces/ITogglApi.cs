namespace CoreySutton.TogglTools.Core
{
    public interface ITogglApi
    {
        Report GetReport(ApiParameters apiParameters);
        Workspaces GetWorkspaces();
    }
}
