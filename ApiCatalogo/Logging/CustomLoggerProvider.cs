using System.Collections.Concurrent;

namespace ApiCatalogo.Logging;

public class CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig) : ILoggerProvider
{
    private readonly CustomLoggerProviderConfiguration _loggerConfig = loggerConfig;;
    private readonly ConcurrentDictionary<string, CustomerLogger> loggers = new();

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
    }

    public void Dispose() => loggers.Clear();
}