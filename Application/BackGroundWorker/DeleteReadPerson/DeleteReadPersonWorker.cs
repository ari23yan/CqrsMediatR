using Application.BaseChannel;
using Infrastructure.Repositories.ReadRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BackGroundWorker.DeleteReadPerson
{
    public class DeleteReadPersonWorker : BackgroundService
    {
        private readonly ChannelQueue<DeletePersonModel> _deleteModelChannel;
        private readonly IServiceProvider _serviceProvider;

        public DeleteReadPersonWorker(ChannelQueue<DeletePersonModel> deleteModelChannel, IServiceProvider serviceProvider)
        {
            _deleteModelChannel = deleteModelChannel;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var readPersonRepository = scope.ServiceProvider.GetRequiredService<ReadPersonRepository>();
                    await foreach (var item in _deleteModelChannel.ReturnValue(stoppingToken))
                    {
                        await readPersonRepository.DeleteByPersonIdAsync(item.Id, stoppingToken);
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

