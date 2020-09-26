using Voting.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Api.Job
{
    [DisallowConcurrentExecution]
    public class AlertJob : IJob
    {
        private readonly IServiceProvider _provider;

        private readonly ILogger<AlertJob> _logger;
        public AlertJob(ILogger<AlertJob> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            // Create a new scope
            using (var scope = _provider.CreateScope())
            {
                // Resolve the Scoped service
                //var service = scope.ServiceProvider.GetRequiredService<INotificationService>();
                //if(service != null)
                //service.CheckPriceAlert();
                _logger.LogInformation("Hello world!");
            }

            return Task.CompletedTask;
        }
    }
}