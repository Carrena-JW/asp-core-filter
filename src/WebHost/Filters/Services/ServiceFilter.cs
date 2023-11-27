using Microsoft.AspNetCore.Mvc.Filters;

namespace WebHost.Filters.Services
{
    public class ServiceFilter : Attribute, IFilterFactory
    {
        private readonly Type _filterType;

        public ServiceFilter(Type filterType)
        {
            _filterType = filterType;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return (IFilterMetadata)serviceProvider.GetRequiredService(_filterType);
        }

        public bool IsReusable => false;
    }
}
