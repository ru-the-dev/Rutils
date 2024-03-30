using Discord;
using Microsoft.Extensions.Logging;

namespace Rutils.Discord;

public static class LogHelper
{
    public static Task OnLogAsync(ILogger logger, LogMessage msg)
    {
        if (msg.Message == null)
        {
            return Task.CompletedTask;
        }
        
        //filter out OPcodes
        //eg. Unknown OpCode (20)
        if (msg.Message.Length >= 14 && msg.Message.Substring(0, 14) == "Unknown OpCode")
        {
            return Task.CompletedTask;
        }

        switch (msg.Severity)
        {
            case LogSeverity.Verbose:
                logger.LogInformation(msg.ToString());
                break;

            case LogSeverity.Info:
                logger.LogInformation(msg.ToString());
                break;

            case LogSeverity.Warning:
                logger.LogWarning(msg.ToString());
                break;

            case LogSeverity.Error:
                logger.LogError(msg.ToString());
                break;

            case LogSeverity.Critical:
                logger.LogCritical(msg.ToString());
                break;
        }
        return Task.CompletedTask;
    }
}