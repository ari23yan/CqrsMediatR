using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.WriteRepository
{
    public class WritePersonRepository
    {
        public readonly ApplicationDbContext _context;
        public WritePersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddMovieAsync(Person person, CancellationToken cancellationToken = default)
        {
            await _context.Persons.AddAsync(person, cancellationToken);
        }

        public Task<Person> GetMovieByIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return _context.Persons.FirstOrDefaultAsync(c => c.Id == personId, cancellationToken);
        }

        public void DeleteMovie(Person person)
        {
            _context.Persons.Remove(person);
        }

    }
}
