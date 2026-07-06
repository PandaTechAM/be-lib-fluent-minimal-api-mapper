using Microsoft.Extensions.Hosting;

namespace FluentMinimalApiMapper;

/// <summary>
///     Configures Minimal API discovery, including which environments may map testing endpoints.
/// </summary>
public sealed class MinimalApiOptions
{
    private readonly HashSet<string> _testingEndpointEnvironments =
        new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    ///     Adds environment names in which <see cref="ITestingEndpoint" /> endpoints are mapped.
    /// </summary>
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
