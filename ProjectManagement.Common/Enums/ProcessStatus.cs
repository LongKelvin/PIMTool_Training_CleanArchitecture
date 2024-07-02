namespace ProjectManagement.Common.Enums
{
    public enum ProcessStatus
    {
        /// <summary>
        /// The process has succeeded.
        /// </summary>
        Succeeded = 0,

        /// <summary>
        /// The process has failed.
        /// </summary>
        Failed = 1,

        /// <summary>
        /// The process is currently running.
        /// </summary>
        Running = 2,

        /// <summary>
        /// The process is idle.
        /// </summary>
        Idle = 3
    }
}