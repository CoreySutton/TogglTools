using System.ComponentModel.DataAnnotations;

namespace CoreySutton.TogglTools.Web
{
    public class WorkspaceRequestFormViewModel
    {
        [Required(ErrorMessage = "You must provide your email address.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must provide your API token.")]
        public string ApiToken { get; set; }
    }
}