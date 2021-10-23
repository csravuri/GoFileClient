using System.Collections.Generic;
using System.Diagnostics;

namespace GoFileClient.Logger
{
    public class Logger : ILogger
    {
        #region ILogger

        void ILogger.Log(string message) => Log(message);

        void ILogger.Log(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                Log(message);
            }
        }

        #endregion ILogger

        #region Implementation

        private void Log(string message)
        {
            Debug.Print(message);
        }

        #endregion Implementation
    }
}