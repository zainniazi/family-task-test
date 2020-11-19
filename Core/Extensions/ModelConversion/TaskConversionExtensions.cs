using Domain.Commands;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions.ModelConversion
{
    public static class TaskConversionExtensions
    {
        public static CreateTaskCommand ToCreateTaskCommand(this CreateTaskVm model)
        {
            var command = new CreateTaskCommand()
            {
                AssignedMemberId = model.AssignedMemberId == Guid.Empty ? null : model.AssignedMemberId,
                Subject = model.Subject
            };
            return command;
        }

        public static AssignMemberCommand ToAssignMemberCommand(this TaskVm model)
        {
            var command = new AssignMemberCommand()
            {
                TaskId = model.Id,
                MemberId = (Guid)model.AssignedMemberId
            };
            return command;
        }
    }
}
