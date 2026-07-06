namespace FluentMinimalApiMapper;

/// <summary>
///     Marks an endpoint that is mapped only in the environments configured via
///     <see cref="MinimalApiOptions.AddTestingEndpointEnvironments" />.
/// </summary>
public interface ITestingEndpoint : IEndpoint
{
}
