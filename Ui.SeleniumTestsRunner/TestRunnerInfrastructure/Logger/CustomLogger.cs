using System.Diagnostics;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Logger
{
    internal class CustomLogger : ILogger
    {
        static CustomLogger()
        {
            //Trace.Listeners.Add(new ConsoleTraceListener());
        }

        public void Log(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
