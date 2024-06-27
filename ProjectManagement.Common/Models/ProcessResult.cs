using ProjectManagement.Common.Enums;

namespace ProjectManagement.Common.Models
{
    public class ProcessResult
    {
        public int PID { get; set; }
        public string ProcessName { get; set; }
        public int ExitCode { get; set; }
        public List<string> StandardOutput { get; set; }
        public List<string> ErrorOutput { get; set; }
        public string Message { get; set; }

        public ProcessStatus ProcessStatus { get; set; }

        public ProcessResult()
        {
            StandardOutput = [];
            ErrorOutput = [];
            Message = string.Empty;
            ProcessName = string.Empty;
            ProcessStatus = ProcessStatus.Idle;
        }

        public ProcessResult(ProcessStatus status)
        {
            ProcessStatus = status;
            StandardOutput = [];
            ErrorOutput = [];
            Message = string.Empty;
            ProcessName = string.Empty;
        }
    }

    public class ProcessResult<T> : ProcessResult where T : class
    {
        public T? ReturnData { get; set; }

        public ProcessResult() : base()
        {
        }

        public ProcessResult(ProcessResult baseResult, T returnData) : base()
        {
            PID = baseResult.PID;
            ProcessName = baseResult.ProcessName;
            ExitCode = baseResult.ExitCode;
            StandardOutput = baseResult.StandardOutput;
            ErrorOutput = baseResult.ErrorOutput;
            Message = baseResult.Message;
            ReturnData = returnData;
        }
    }

    public class ProcessInvokerOptions
    {
        public string? WorkingDirectory { get; set; }
        public bool AsyncPrintEnabled { get; set; }
        public int TimeoutMilliseconds { get; set; } = -1;
    }
}
