using SimplexInvoice.Application.Common.Interfaces.Persistance;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog;
using System;

namespace SimplexInvoice.Infra.Services
{
    public class LoggerSeriLog : IDisposable, ICustomLogger
    {
        private readonly Logger _logger;

        public LoggerSeriLog(IConfiguration configuration)
        {            
            _logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                                               .CreateLogger();
            _logger.Information($"Logging created");
        }

        public void Dispose() => _logger.Dispose();
        public void Information(string message) => _logger.Information(message);
        public void Information<T>(string message, T propertyValue) => _logger.Information<T>(message, propertyValue);
        public void Warning(string message) => _logger.Warning(message);
        public void Warning<T>(string message, T propertyValue) => _logger.Warning<T>(message, propertyValue);
        public void Debug(string message) => _logger.Debug(message);
        public void Debug<T>(string message, T propertyValue) => _logger.Debug<T>(message, propertyValue);
        public void Error(string message) => _logger.Error(message);
        public void Error<T>(string message, T propertyValue) => _logger.Error<T>(message, propertyValue);
        public void Fatal(string message) => _logger.Fatal(message);
        public void Fatal<T>(string message, T propertyValue) => _logger.Fatal<T>(message, propertyValue);
        public void Verbose(string message) => _logger.Verbose(message);
        public void Verbose<T>(string message, T propertyValue) => _logger.Verbose<T>(message, propertyValue);
    }
}