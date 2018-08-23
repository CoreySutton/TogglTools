using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Core
{
    public class ApiContext
    {
        public ApiPaths Paths;
        public ApiCredentials Credentials;
        public ApiParameters Parameters;

        public ApiContext(ApiPaths paths, ApiCredentials credentials, ApiParameters parameters = null)
        {
            Argument.IsNotNull(paths, nameof(paths));
            Argument.IsNotNull(credentials, nameof(credentials));

            Paths = paths;
            Credentials = credentials;
            Parameters = parameters ?? new ApiParameters();
        }
    }
}