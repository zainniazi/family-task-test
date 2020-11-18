using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository: IBaseRepository<Guid, Task, ITaskRepository>
    {
        System.Threading.Tasks.Task<IEnumerable<Task>> GetAllByMemberId(Guid memberId, CancellationToken cancellationToken = default);
    }
}