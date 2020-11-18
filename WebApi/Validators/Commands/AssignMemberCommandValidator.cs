using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Validators.Commands
{
    public class AssignMemberCommandValidator : AbstractValidator<AssignMemberCommand>
    {
        public AssignMemberCommandValidator()
        {
            RuleFor(x => x.TaskId).NotNull().NotEmpty().NotEqual(default(Guid));
            RuleFor(x => x.MemberId).NotNull().NotEmpty().NotEqual(default(Guid));
        }
    }
}
