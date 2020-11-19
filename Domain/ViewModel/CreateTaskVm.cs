using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.ViewModel
{
    public class CreateTaskVm
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }
        public Guid? AssignedMemberId { get; set; }
    }
}
