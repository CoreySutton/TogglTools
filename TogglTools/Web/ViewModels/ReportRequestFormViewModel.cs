using System;
using System.ComponentModel.DataAnnotations;

namespace CoreySutton.TogglTools.Web
{
    public class ReportRequestFormViewModel
    {
        [Required(ErrorMessage = "You must provide your email address.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must provide your API token.")]
        public string ApiToken { get; set; }

        [Required(ErrorMessage = "You must provide a workspace id.")]
        public string WorkspaceId { get; set; }

        [Required(ErrorMessage = "You must provide a value for round at.")]
        public double RoundAt { get; set; }

        [Required(ErrorMessage = "You must provide a value for round to.")]
        public double RoundTo { get; set; }

        [Required(ErrorMessage = "You must provide a range start.")]
        public DateTime RangeStart { get; set; }

        [Required(ErrorMessage = "You must provide a range end.")]
        public DateTime RangeEnd { get; set; }

        public ReportRequestFormViewModel(string email, string apiToken)
        {
            Email = email;
            ApiToken = apiToken;
        }

        public ReportRequestFormViewModel()
        {

        }
    }
}