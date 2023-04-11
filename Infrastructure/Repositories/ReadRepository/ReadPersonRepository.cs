using Domain.Entities;
using Domain.Repositories.ReadRepository.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ReadRepository
{
    public class ReadPersonRepository : BaseReadRepository<Person>
    {
        public ReadPersonRepository(IMongoDatabase db) : base(db)
        {
        }
        public Task<Person> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return base.FirstOrDefaultAsync(person => person.Id == personId, cancellationToken);
        }

        public Task<Person> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return base.FirstOrDefaultAsync(person => person.Email == email, cancellationToken);
        }

        public Task DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(m => m.Id == personId, cancellationToken);
        }
    }
}
