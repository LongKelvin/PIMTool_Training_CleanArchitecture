using System.Diagnostics;

namespace ProjectManagement.Api.Extensions
{
    public static class DebugConsoleWindowHandler
    {
        private static Process? _powershellProcess;

        public static void AttachConsole()
        {
            if (_powershellProcess == null || _powershellProcess.HasExited)
            {
                _powershellProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = "-NoExit -Command \"Write-Host 'Debug Console Attached'\"",
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                _powershellProcess.Start();
            }
        }

        public static void WriteLine(string message)
        {
            if (_powershellProcess != null && !_powershellProcess.HasExited)
            {
                _powershellProcess.StandardInput.WriteLine(message);
            }
        }

        public static bool ShouldAttachConsole()
        {
            var attachConsole = Environment.GetEnvironmentVariable("ATTACH_CONSOLE");
            return attachConsole != null && attachConsole.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public static void CloseConsole()
        {
            if (_powershellProcess != null && !_powershellProcess.HasExited)
            {
                _powershellProcess.StandardInput.WriteLine("exit");
                _powershellProcess.WaitForExit();
                _powershellProcess.Close();
                _powershellProcess = null;
            }
        }
    }
}
