using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class AssignMemberCommandResult : BaseTaskCommandResult
    {
        public string Message { get; set; }
    }
}
