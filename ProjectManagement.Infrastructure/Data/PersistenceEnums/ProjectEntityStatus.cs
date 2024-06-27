using System.ComponentModel;

namespace ProjectManagement.Infrastructure.Data.PersistenceEnums
{
    public enum ProjectEntityStatus
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
