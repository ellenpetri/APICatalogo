namespace ApiCatalogo.Logging;

public class CustomerLogger(string name, CustomLoggerProviderConfiguration config) : ILogger
{
    private readonly string _loggerName = name;

    private readonly CustomLoggerProviderConfiguration loggerConfig = config;

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }
    private void EscreverTextoNoArquivo(string mensagem)
    {
        string camimhoArquivoLog = @"C:\Temp\ApiCatalogo_Log.txt";

        StreamWriter streamWriter = new(camimhoArquivoLog);

        try
        {
            streamWriter.Write(mensagem);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            streamWriter.Dispose();
        }
    }
}
