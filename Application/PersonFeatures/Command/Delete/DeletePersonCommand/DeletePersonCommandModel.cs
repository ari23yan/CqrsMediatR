using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Command.Delete.DeletePersonCommand
{
    public class DeletePersonCommandModel : IRequest<Guid>
    {
        public Guid Id { get; set; }

    }
}
