using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkorderService.DomainServices;
using WorkorderService.Messages;

namespace WorkorderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkorderController : ControllerBase
    {
        private readonly ILogger<WorkorderController> _logger;
        private readonly MessagingService _messagingService;

        public WorkorderController(ILogger<WorkorderController> logger, MessagingService messagingService)
        {
            _logger = logger;
            _messagingService = messagingService;
        }

        [HttpPost]
        public async Task CreateWorkorder(string clientCode)
        {
            _logger.LogInformation($"Received request to create workorder for client {clientCode}");

            Thread.Sleep(2000);

            await _messagingService.Publish(new WorkorderCommitted
            {
                WorkorderId = Guid.NewGuid().ToString(),
                ClientCode = clientCode
            });
        }
    }
}
