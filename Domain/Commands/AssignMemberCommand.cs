using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class AssignMemberCommand
    {
        public Guid TaskId { get; set; }
        public Guid MemberId { get; set; }
    }
}
