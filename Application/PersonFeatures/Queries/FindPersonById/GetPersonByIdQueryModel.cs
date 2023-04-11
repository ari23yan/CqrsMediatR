using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonFeatures.Queries.FindPersonById
{
    public class GetPersonByIdQueryModel : IRequest<Person>
    {
        public Guid Id { get; set; }

    }
}
