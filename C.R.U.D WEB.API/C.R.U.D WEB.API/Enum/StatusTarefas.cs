using System.ComponentModel;

namespace C.R.U.D_WEB.API.Enum
{
    public enum StatusTarefas
    {
        [Description("To Do")]
        ToDo =1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Done")]
        Done = 3,
    }
}
