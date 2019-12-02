using System;
using System.Net.Http;
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
        public async Task<IActionResult> CreateWorkorder(string clientCode)
        {
            _logger.LogInformation($"Received request to create workorder for client {clientCode}");

            if (!ClientService.DoesClientExist(clientCode))
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
