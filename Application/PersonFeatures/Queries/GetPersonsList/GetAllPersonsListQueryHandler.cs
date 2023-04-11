using Domain.Entities;
using Infrastructure.Repositories.ReadRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Queries.GetPersonsList
{
    public class GetAllPersonsListQueryHandler : IRequestHandler<GetAllPersonsListQueryModel, IEnumerable<Person>>
    {
        private readonly ReadPersonRepository _readPerson;

        public GetAllPersonsListQueryHandler(ReadPersonRepository readPerson)
        {
            _readPerson = readPerson;
        }
        public async Task<IEnumerable<Person>> Handle(GetAllPersonsListQueryModel request, CancellationToken cancellationToken)
        {
            var personList = await _readPerson.GetAllAsync();
            if (personList == null)
            {
                return null;
            }
            return personList.AsReadOnly();
        }
    }
}
