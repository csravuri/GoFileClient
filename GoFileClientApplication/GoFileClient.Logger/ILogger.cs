using System.Collections.Generic;

namespace GoFileClient.Logger
{
    public interface ILogger
    {
        void Log(string message);

        void Log(IEnumerable<string> messages);
    }
}