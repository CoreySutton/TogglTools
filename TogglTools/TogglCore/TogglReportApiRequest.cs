namespace CoreySutton.TogglTools.TogglCore
{
    public class TogglReportApiRequest : ITogglApiRequest
    {
        private readonly ApiContext _apiContext;

        public TogglReportApiRequest(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public Report GetReport()
        {
            throw new System.NotImplementedException();
        }

        public Workspaces GetWorkspaces()
        {
            throw new System.NotImplementedException();
        }
    }
}
