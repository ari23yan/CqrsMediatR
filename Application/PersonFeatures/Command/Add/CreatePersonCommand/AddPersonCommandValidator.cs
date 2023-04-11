using FluentValidation;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Command.Add.CreatePersonCommand
{
    internal class AddPersonCommandValidator : AbstractValidator<AddPersonCommandModel>
    {
        private readonly ApplicationDbContext _context;
        public AddPersonCommandValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Email)
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.")
           .EmailAddress().WithMessage("The Email Format Is not Correct.")
           .NotEmpty().WithMessage("The Email Cant Be Null");
            RuleFor(v => v.MobileNumber).NotEmpty().WithMessage("The Email Cant Be Null");
        }
        public async Task<bool> BeUniqueTitle(string email, CancellationToken cancellationToken)
        {
            return await _context.Persons
                .AllAsync(l => l.Email != email, cancellationToken);
        }

    }
}
