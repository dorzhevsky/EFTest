using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace BooksEditor.DataAccess.Utils
{
    public class TraceLogger : ILogger
    {
        private readonly string categoryName;

        public TraceLogger(string categoryName) => this.categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            File.AppendAllText(@"C:\tmp\db.log",$"{DateTime.Now.ToString("o")} {logLevel} {eventId.Id} {this.categoryName}");
            File.AppendAllText(@"C:\tmp\db.log", formatter(state, exception));
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }

    public class TraceLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new TraceLogger(categoryName);

        public void Dispose() { }
    }
}