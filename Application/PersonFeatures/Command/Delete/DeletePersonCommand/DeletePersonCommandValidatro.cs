using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Command.Delete.DeletePersonCommand
{
    public class DeletePersonCommandValidatro : AbstractValidator<DeletePersonCommandModel>
    {
        public DeletePersonCommandValidatro()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
