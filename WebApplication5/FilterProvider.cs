using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace WebApplication5
{
    public class FilterProvider : IFilterProvider
    {
        private IUnityContainer container;

        public FilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return container.ResolveAll<IActionFilter>().Select(actionFilter => new Filter(actionFilter, FilterScope.First, null));
        }
    }
}