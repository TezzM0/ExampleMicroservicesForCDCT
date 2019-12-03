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
        private readonly IClientService _clientService;

        public WorkorderController(
            ILogger<WorkorderController> logger, MessagingService messagingService, IClientService clientService)
        {
            _logger = logger;
            _messagingService = messagingService;
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkorder(string clientCode)
        {
            _logger.LogInformation($"Received request to create workorder for client {clientCode}");

            if (!_clientService.DoesClientExist(clientCode))
            {
                return BadRequest("Client code does not exist");
            }

            Thread.Sleep(2000);

            var newWorkorderId = Guid.NewGuid().ToString();
            await _messagingService.Publish(new WorkorderCommitted
            {
                WorkorderId = newWorkorderId,
                ClientCode = clientCode
            });

            return new JsonResult(new WorkorderCreated { Id = newWorkorderId });
        }
    }
}
