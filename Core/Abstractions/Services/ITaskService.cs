using Domain.Commands;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        Task<ToggleTaskCommandResult> ToggleTaskCommandHandler(Guid id);
        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
        Task<GetAllTasksByMemberQueryResult> GetAllTasksByMemberQueryHandler(Guid memberId);
        Task<AssignMemberCommandResult> AssignMemberCommandHandler(AssignMemberCommand command);
    }
}