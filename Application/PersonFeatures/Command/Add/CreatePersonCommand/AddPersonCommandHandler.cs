using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Command.Add.CreatePersonCommand
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommandModel,Guid>
    {
        private readonly ApplicationDbContext _context;
        private protected IMapper _mapper;

        public AddPersonCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddPersonCommandModel request, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<AddPersonCommandModel, Person>(request);
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }
    }
}
