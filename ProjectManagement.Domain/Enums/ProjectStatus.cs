using System.ComponentModel;

namespace ProjectManagement.Domain.Enums
{
    public enum ProjectStatus
    {
        [Description("New")]
        NEW,

        [Description("Planned")]
        PLA,

        [Description("In Progress")]
        INP,

        [Description("Finished")]
        FIN
    }
}
