using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Core
{
    public class ApiCredentials
    {
        public string ApiKey;
        public string Email;
        public string ApiPassword;

        public ApiCredentials(string apiKey, string email)
        {
            Argument.IsNotNullOrEmpty(apiKey, nameof(apiKey));
            Argument.IsNotNullOrEmpty(email, nameof(email));

            ApiKey = apiKey;
            Email = email;
        }
    }
}
