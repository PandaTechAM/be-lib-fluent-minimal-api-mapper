using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentMinimalApiMapper
{
    public sealed class MinimalApiOptions
    {
        private readonly HashSet<string> _testingEndpointEnvironments =
           new(StringComparer.OrdinalIgnoreCase);

        public MinimalApiOptions AddTestingEndpointEnvironments(params string[] environmentNames)
        {
            foreach (var environmentName in environmentNames)
            {
                if (!string.IsNullOrWhiteSpace(environmentName))
                {
                    _testingEndpointEnvironments.Add(environmentName.Trim());
                }
            }

            return this;
        }

        internal bool CanRegisterTestingEndpoints(IHostEnvironment environment)
        {
            return _testingEndpointEnvironments.Contains(environment.EnvironmentName);
        }
    }
}
