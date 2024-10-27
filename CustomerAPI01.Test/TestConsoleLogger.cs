using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI01.Test
{
    using Microsoft.Extensions.Logging;
    using System;

    public class TestConsoleLogger<T> : ILogger<T>
    {
        private readonly List<(LogLevel, string)> _loggedMessages = new List<(LogLevel, string)>();

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            _loggedMessages.Add((logLevel, message));
            Console.WriteLine($"{logLevel}: {message}");
        }

        public IEnumerable<(LogLevel, string)> GetLoggedMessages()
        {
            return _loggedMessages.AsEnumerable();
        }
    }

}
