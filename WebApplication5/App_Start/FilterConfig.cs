using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using WebApplication5.Filters;

namespace WebApplication5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterGlobalFiltersWithDependencies(HttpFilterCollection filters,
            IUnityContainer container)
        {
            filters.Add(container.Resolve<CustomAuthFilter>());
        }
    }
}