using System.Web;
using System.Web.Mvc;
using VR.MVC.Filters;

namespace VR.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new AutorizacaoCustomizada());
        }
    }
}
