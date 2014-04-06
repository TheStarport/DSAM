using LogDispatcher;

namespace DSAccountManager_v2
{
    class LogMessage
    {

        public LogMessage(LogType type, string msg)
        {
            Type = type.ToString();
            Message = msg;
        }

        string Type { get; set; }
        string Message { get; set; }
    }
}
