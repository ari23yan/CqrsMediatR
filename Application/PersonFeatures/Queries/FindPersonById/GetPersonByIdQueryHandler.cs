using Domain.Entities;
using Infrastructure.Repositories.ReadRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Queries.FindPersonById
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQueryModel,Person>
    {
        private readonly ReadPersonRepository _readPerson;

        public GetPersonByIdQueryHandler(ReadPersonRepository readPerson)
        {
            _readPerson = readPerson;
        }

        public async Task<Person> Handle(GetPersonByIdQueryModel request, CancellationToken cancellationToken)
        {
            //var person = _context.Persons.Where(a => a.Id == request.Id).FirstOrDefault();
            var person = await _readPerson.GetByPersonIdAsync(request.Id);
            if (person == null)
            {
                return null;
            }
            return person;
        }
    }
}
