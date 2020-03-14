using Clean.UI.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Clean.UI.Utilities
{
    public class CultureLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public CultureLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(ViewResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("ViewResource", assemblyName.Name);
        }
        public LocalizedString Text(string key, params string[] arguments)
        {
            return arguments == null
                ? _localizer[key]
                : _localizer[key, arguments];
        }
    }
}
