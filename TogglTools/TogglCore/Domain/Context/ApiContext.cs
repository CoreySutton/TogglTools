using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglCore
{
    public class ApiContext
    {
        public ApiPaths Paths;
        public ApiCredentials Credentials;
        public ApiParameters Parameters;

        public ApiContext(ApiPaths paths, ApiCredentials credentials, ApiParameters parameters = null)
        {
            ArgUtil.NotNull(paths, nameof(paths));
            ArgUtil.NotNull(credentials, nameof(credentials));

            Paths = paths;
            Credentials = credentials;
            Parameters = parameters ?? new ApiParameters();
        }
    }
}