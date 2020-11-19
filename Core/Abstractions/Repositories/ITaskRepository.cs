using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository: IBaseRepository<Guid, Task, ITaskRepository>
    {
        System.Threading.Tasks.Task<IEnumerable<Task>> GetAllByMemberId(Guid memberId, CancellationToken cancellationToken = default);
        ITaskRepository Include<TProperty>(Expression<Func<Task, TProperty>> navigationPropertyPath);
    }
}