using CoreySutton.Utilities;
#nullable enable
namespace CoreySutton.TogglTools.Core
{
    public class ApiCredentials
    {
        public string ApiKey;
        public string Email;
        public string ApiPassword;

        public ApiCredentials(string apiKey, string email)
        {
            ApiKey = apiKey;
            Email = email;
            ApiPassword = string.Empty;
        }
    }
}
