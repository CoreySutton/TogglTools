using System.Web;
using System.Web.Mvc;

namespace CoreySutton.TogglTools.Web22
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
