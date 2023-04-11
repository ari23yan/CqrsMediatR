using Application.PersonFeatures.Command.Delete.DeletePersonCommand;
using Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Command.Delete.DeletePersonCommandHandler
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommandModel, Guid>
    {
        private readonly ApplicationDbContext _context;
        public DeletePersonCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeletePersonCommandModel request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.Where(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (person == null) return default;
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }
    }
}
