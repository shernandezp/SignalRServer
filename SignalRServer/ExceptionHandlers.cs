namespace SignalRServer;

public static class ExceptionHandlers
{
    public static async Task HandleExceptionAsync(Func<Task> action, ILogger logger)
    {
        try
        {
            await action.Invoke();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message} - {StackTrace}", ex.Message, ex.StackTrace);
        }
    }
    public static void HandleException(Action action, ILogger logger)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message} - {StackTrace}", ex.Message, ex.StackTrace);
        }
    }
}
