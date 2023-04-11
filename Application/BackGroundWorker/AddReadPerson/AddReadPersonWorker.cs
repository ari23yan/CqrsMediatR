using Application.BaseChannel;
using Domain.Entities;
using Infrastructure.Repositories.ReadRepository;
using Infrastructure.Repositories.WriteRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BackGroundWorker.AddReadPerson
{
    public class AddReadPersonWorker : BackgroundService
    {
        private readonly ChannelQueue<AddPersonModel> _readModelChannel;
        private readonly IServiceProvider _serviceProvider;
        public AddReadPersonWorker(ChannelQueue<AddPersonModel> readModelChannel, IServiceProvider serviceProvider)
        {
            _readModelChannel = readModelChannel;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var writeRepository = scope.ServiceProvider.GetRequiredService<WritePersonRepository>();
                var readMovieRepository = scope.ServiceProvider.GetRequiredService<ReadPersonRepository>();
                try
                {
                    await foreach (var item in _readModelChannel.ReturnValue(stoppingToken))
                    {
                        var person = await writeRepository.GetMovieByIdAsync(item.Id, stoppingToken);

                        if (person != null)
                        {
                            await readMovieRepository.AddAsync(new Person
                            {
                                Email = person.Email,
                                MobileNumber = person.MobileNumber,
                                Family = person.Family,
                                NationalCode = person.NationalCode,
                                Name = person.Name,
                                Password = person.Password,
                                Gender = person.Gender
                            }, stoppingToken);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Could not update the entity {e.Message}");

                }
            }
        }
    }
}
