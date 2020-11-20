using AutoMapper;
using Core;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.DataModels;
using Domain.Queries;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper, ITaskRepository taskRepository, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _memberRepository = memberRepository;
        }

        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            if (command.AssignedMemberId != null)
            {
                var memberExist = await _memberRepository.ExistsAsync((Guid)command.AssignedMemberId);
                if (!memberExist)
                    throw new NotFoundException<Guid>(typeof(Member).Name, (Guid)command.AssignedMemberId);
            }
            var task = _mapper.Map<Domain.DataModels.Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);

            var vm = _mapper.Map<TaskVm>(persistedTask);
            return new CreateTaskCommandResult()
            {
                Payload = vm
            };
        }

        public async Task<ToggleTaskCommandResult> ToggleTaskCommandHandler(Guid id)
        {
            bool isSucceed = true;
            var task = await _taskRepository.ByIdAsync(id);
            task.IsComplete = !task.IsComplete;
            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new ToggleTaskCommandResult()
            {
                Succeed = isSucceed
            };
        }

        public async Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            IEnumerable<TaskVm> vm = new List<TaskVm>();
            var tasks = await _taskRepository.Reset().Include(x=>x.AssignedMember).ToListAsync();

            if (tasks != null && tasks.Any())
                vm = _mapper.Map<IEnumerable<TaskVm>>(tasks);

            return new GetAllTasksQueryResult()
            {
                Payload = vm
            };
        }

        public async Task<GetAllTasksByMemberQueryResult> GetAllTasksByMemberQueryHandler(Guid memberId)
        {
            IEnumerable<TaskVm> vm = new List<TaskVm>();
            var tasks = await _taskRepository.Reset().Include(x => x.AssignedMember).GetAllByMemberId(memberId);

            if (tasks != null && tasks.Any())
                vm = _mapper.Map<IEnumerable<TaskVm>>(tasks);

            return new GetAllTasksByMemberQueryResult()
            {
                Payload = vm
            };
        }

        public async Task<AssignMemberCommandResult> AssignMemberCommandHandler(AssignMemberCommand command)
        {
            var task = await _taskRepository.ByIdAsync(command.TaskId);
            var memberExist = await _memberRepository.ExistsAsync(command.MemberId);
            if (!memberExist)
                return new AssignMemberCommandResult { Succeed = false, Message = "Invalid member" };

            task.AssignedMemberId = command.MemberId;
            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            var result = new AssignMemberCommandResult { Succeed = true };
            if (affectedRecordsCount < 1)
            {
                result.Succeed = false;
                result.Message = "Something went wrong.";
            }
            return result;
        }
    }
}