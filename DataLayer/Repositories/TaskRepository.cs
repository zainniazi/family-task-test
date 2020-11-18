using Core.Abstractions.Repositories;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace DataLayer.Repositories
{
    public class TaskRepository : BaseRepository<Guid, Task, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context)
        {
        }

        ITaskRepository IBaseRepository<Guid, Task, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, Task, ITaskRepository>.Reset()
        {
            return base.Reset();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetAllByMemberId(Guid memberId, CancellationToken cancellationToken = default)
        {
            return await Query.Where(x => x.AssignedMemberId == memberId).ToListAsync(cancellationToken);
        }

        public ITaskRepository Include<TProperty>(Expression<Func<Task, TProperty>> navigationPropertyPath)
        {
            Query = Query.Include(navigationPropertyPath).AsQueryable();
            return this;
        }
    }
}
