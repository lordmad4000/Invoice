using SimplexInvoice.Application.Common.Interfaces.Persistance;

namespace SimplexInvoice.Api.Tests.IntegrationTests.Common
{
    internal class TestsLogger : ICustomLogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Debug<T>(string message, T propertyValue)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error<T>(string message, T propertyValue)
        {
            Console.WriteLine(message);
        }

        public void Fatal(string message)
        {
            Console.WriteLine(message);
        }

        public void Fatal<T>(string message, T propertyValue)
        {
            Console.WriteLine(message);
        }

        public void Information(string message)
        {
            Console.WriteLine(message);
        }

        public void Information<T>(string message, T propertyValue)
        {
            Console.WriteLine(message);
        }

        public void Verbose(string message)
        {
            Console.WriteLine(message);
        }

        public void Verbose<T>(string message, T propertyValue)
        {
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            Console.WriteLine(message);
        }

        public void Warning<T>(string message, T propertyValue)
        {
            Console.WriteLine(message);
        }
    }
}
