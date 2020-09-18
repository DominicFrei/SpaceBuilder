public static class Logger

    // TODO line numbers
{
    public static LogLevel DefaultLogLevel = LogLevel.Debug;

    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Error(string message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Warning(string message)
    {
        if (DefaultLogLevel <= LogLevel.Warning)
        {
            UnityEngine.Debug.LogWarning(message);
        }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Info(string message)
    {
        if (DefaultLogLevel <= LogLevel.Info)
        {
            UnityEngine.Debug.Log(message);
        }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Debug(string message)
    {
        if (DefaultLogLevel <= LogLevel.Debug)
        {
            UnityEngine.Debug.Log(message);
        }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Trace(string message)
    {
        if (DefaultLogLevel == LogLevel.Trace)
        {
            UnityEngine.Debug.Log(message);
        }
    }


}