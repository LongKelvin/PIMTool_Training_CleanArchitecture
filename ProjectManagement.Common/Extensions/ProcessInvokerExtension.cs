using System.Collections.Concurrent;
using System.Diagnostics;

using ProjectManagement.Common.Enums;
using ProjectManagement.Common.Models;

namespace ProjectManagement.Common.Extensions
{
    public static class ProcessInvoker
    {
        private static readonly ConcurrentDictionary<int, Process> _runningProcesses = new();
        private static readonly ConcurrentDictionary<int, bool> _isCancelRequested = new();

        public static async Task<ProcessResult> InvokeCommandAsync(string command, string arguments,
            ProcessInvokerOptions? options = null)
        {
            options ??= new ProcessInvokerOptions(); // Provide default options if not specified

            var result = new ProcessResult();

            try
            {
                result.ProcessStatus = ProcessStatus.Idle;

                using var process = new Process();
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = options.WorkingDirectory;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        result.StandardOutput.Add(e.Data);
                    }
                };
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        result.ErrorOutput.Add(e.Data);
                    }
                };

                Console.WriteLine($"Starting process: {command} {arguments}");

                process.Start();
                result.ProcessStatus = ProcessStatus.Running;

                // Assign processId to result
                result.PID = process.Id;
                result.ProcessName = process.ProcessName;

                _runningProcesses.TryAdd(process.Id, process);
                _isCancelRequested.TryAdd(process.Id, false);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (options.TimeoutMilliseconds == -1)
                {
                    process.WaitForExit();
                }
                else
                {
                    if (!await Task.Run(() => process.WaitForExit(options.TimeoutMilliseconds)))
                    {
                        KillProcess(process.Id);
                        result.ExitCode = -1;
                        result.Message = "Process timed out";
                        result.ProcessStatus = ProcessStatus.Failed;
                        return result;
                    }
                }

                // Ensure all output is read before exiting
                process.WaitForExit();

                result.ExitCode = process.ExitCode;

                if (process.ExitCode != 0)
                {
                    result.Message = "Process execution failed";
                    result.ProcessStatus = ProcessStatus.Failed;
                }
                else
                {
                    result.ProcessStatus = ProcessStatus.Succeeded;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"ERROR: {ex.Message}";
                result.ExitCode = -1;
                result.ProcessStatus = ProcessStatus.Failed;
                result.ErrorOutput.Add(ex.ToString());
            }
            finally
            {
                _runningProcesses.TryRemove(result.PID, out _);
                _isCancelRequested.TryRemove(result.PID, out _);
            }

            return result;
        }

        public static async Task<ProcessResult> InvokeCommandAsync(string command, Action<string>? callback = null, int timeOut = -1)
        {
            var result = new ProcessResult();

            try
            {
                result.ProcessStatus = ProcessStatus.Idle;

                using var process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/c {command}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        result.StandardOutput.Add(e.Data);
                        callback?.Invoke(e.Data);
                    }
                };
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        result.ErrorOutput.Add(e.Data);
                        callback?.Invoke(e.Data);
                    }
                };

                process.Start();
                result.ProcessStatus = ProcessStatus.Running;

                // Assign processId to result
                result.PID = process.Id;
                result.ProcessName = process.ProcessName;

                _runningProcesses.TryAdd(process.Id, process);
                _isCancelRequested.TryAdd(process.Id, false);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (timeOut == -1)
                {
                    await Task.Run(() => process.WaitForExit());
                }
                else
                {
                    if (!await Task.Run(() => process.WaitForExit(timeOut)))
                    {
                        KillProcess(process.Id);
                        result.ExitCode = -1;
                        result.Message = "Process timed out";
                        result.ProcessStatus = ProcessStatus.Failed;
                        return result;
                    }
                }

                // Ensure all output is read before exiting
                process.WaitForExit();

                result.ExitCode = process.ExitCode;

                // Handle the custom normal exit code 100

                if (process.ExitCode != 0)
                {
                    result.Message = "Process execution failed";
                    result.ProcessStatus = ProcessStatus.Failed;
                }
                else
                {
                    result.Message = "Process execution succeeded";
                    result.ProcessStatus = ProcessStatus.Succeeded;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"ERROR: {ex.Message}";
                result.ExitCode = -1;
                result.ProcessStatus = ProcessStatus.Failed;
                result.ErrorOutput.Add(ex.ToString());
            }
            finally
            {
                _runningProcesses.TryRemove(result.PID, out _);
                _isCancelRequested.TryRemove(result.PID, out _);
            }

            return result;
        }

        public static void CancelAsync(int processId)
        {
            if (_runningProcesses.TryGetValue(processId, out var process))
            {
                _isCancelRequested[processId] = true;
                process.Kill();
            }
        }

        private static void KillProcess(int processId)
        {
            if (_runningProcesses.TryGetValue(processId, out var process))
            {
                process.Kill();
                _runningProcesses.TryRemove(processId, out _);
            }
        }
    }
}
