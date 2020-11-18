using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class AssignMemberCommandResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
    }
}
