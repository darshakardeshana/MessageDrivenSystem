using MessageConsumer.Domain.Entities;
using MessageConsumer.Infrastructure.Services;
using MessageConsumer.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageConsumer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IResultProcessingService _metricsService;

        public MetricsController(IResultProcessingService metricsService)
        {
            _metricsService = metricsService;
        }

        [HttpGet("summary")]
        public ActionResult<ResultSummary> GetSummary()
        {
            List<MessageProcessingResult> totalProcessedMessages = _metricsService.GetAllProcessedMessages();
            List<MessageProcessingResult> totalSuccessMessages = _metricsService.GetSuccessMessage();
            List<MessageProcessingResult> totalFailedMessages = _metricsService.GetFailedMessages();

            ResultSummary resultSummary = new ResultSummary(totalProcessedMessages.Count, totalSuccessMessages.Count, totalFailedMessages.Count);

            return Ok(resultSummary);
        }
        
        [HttpGet("processed-messages")]
        public ActionResult<List<MessageProcessingResult>> GetProcessedMessages()
        {
            return Ok(_metricsService.GetAllProcessedMessages());
        }

        [HttpGet("total-failed-messages")]
        public ActionResult<int> GetTotalFailed()
        {
            return Ok(_metricsService.GetFailedMessages());
        }

        [HttpGet("total-success-message")]
        public ActionResult<string?> GetLastProcessedMessage()
        {
            return Ok(_metricsService.GetSuccessMessage());
        }
    }
}
