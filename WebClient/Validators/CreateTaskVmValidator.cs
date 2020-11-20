using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ViewModel;
using FluentValidation;

namespace WebClient.Validators
{
    public class CreateTaskVmValidator : AbstractValidator<CreateTaskVm>
    {
        public CreateTaskVmValidator()
        {
            RuleFor(t => t.Subject).NotEmpty().WithMessage("Subject is required.");
        }
    }
}
