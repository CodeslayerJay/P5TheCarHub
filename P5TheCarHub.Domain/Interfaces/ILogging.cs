using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces
{
    public interface ILogging<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}
