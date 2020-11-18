using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.ViewModel
{
    public class CreateTaskVm
    {
        public string Subject { get; set; }
        public Guid? AssignedMemberId { get; set; }
    }
}
