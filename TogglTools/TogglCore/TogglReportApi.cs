namespace CoreySutton.TogglTools.Core
{
    public class TogglReportApi : ITogglApi
    {
        private readonly ApiContext _apiContext;

        public TogglReportApi(ApiContext apiContext)
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
