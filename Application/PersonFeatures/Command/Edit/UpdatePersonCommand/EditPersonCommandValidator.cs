using Application.PersonFeatures.Command.Delete.DeletePersonCommand;
using FluentValidation;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Command.Edit.UpdatePersonCommand
{
    public class EditPersonCommandValidator : AbstractValidator<EditPersonCommandModel>
    {
        private readonly ApplicationDbContext _Context;

        public EditPersonCommandValidator(ApplicationDbContext context)
        {
            _Context = context;
            RuleFor(v => v.MobileNumber)
                .NotEmpty().WithMessage("the mobile value  cant be nul")
                .MaximumLength(12).WithMessage("the Mobile cant be more than 12 number ");

            RuleFor(v => v.NationalCode)
             .NotEmpty().WithMessage("the NationalCode value  cant be nul")
              .MaximumLength(10).WithMessage("the NationalCode cant be more than 10 number ");

             RuleFor(v => v.Email)
          .NotEmpty().WithMessage("the Email value  cant be nul")
           .MustAsync(EmailNeddBeUniqe).WithMessage("The Email Must be Uniqe");
        }

        public async Task<bool> EmailNeddBeUniqe(string email, CancellationToken cancellationToken)
        {
            return await _Context.Persons
          .AllAsync(l => l.Email != email, cancellationToken);
        }
    }
}
