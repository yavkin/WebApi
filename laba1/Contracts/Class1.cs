using System;

namespace Contracts {
    public class Class1 {
        public interface ILoggerManager {
            void LogInfo(string message);
            void LogWarn(string message);
            void LogDebug(string message);
            void LogError(string message);
        }
    }
}