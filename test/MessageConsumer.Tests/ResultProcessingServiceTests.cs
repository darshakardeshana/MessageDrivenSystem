using MessageConsumer.Domain.Entities;
using MessageConsumer.Infrastructure.Services.Implementations;

namespace MessageConsumer.Tests
{
    public class ResultProcessingServiceTests
    {
        private readonly ResultProcessingService _resultProcessingService;
        private readonly Message _testMessage = new Message { Content = "Test Content" };
        private const int _processingTime = 5;
        private const string _errorMessage = "A test error occurred.";

        public ResultProcessingServiceTests()
        {
            _resultProcessingService = new ResultProcessingService();
        }

        [Fact]
        public void AddMessageProcessingResult_AddsSuccessResultToList()
        {
            var result = MessageProcessingResult.BuildSuccessResult(_testMessage, _processingTime);

            _resultProcessingService.AddMessageProcessingResult(result);

            var allMessages = _resultProcessingService.GetAllProcessedMessages();
            Assert.Single(allMessages);
            Assert.Contains(result, allMessages);
        }

        [Fact]
        public void AddMessageProcessingResult_AddsErrorResultToList()
        {
            var result = MessageProcessingResult.BuildErrorResult(_testMessage, _processingTime, _errorMessage);

            _resultProcessingService.AddMessageProcessingResult(result);

            var allMessages = _resultProcessingService.GetAllProcessedMessages();
            Assert.Single(allMessages);
            Assert.Contains(result, allMessages);
        }

        [Fact]
        public void GetAllProcessedMessages_ReturnsAllAddedResults()
        {
            var successResult = MessageProcessingResult.BuildSuccessResult(new Message { Content = "Success 1" }, 3);
            var errorResult = MessageProcessingResult.BuildErrorResult(new Message { Content = "Error 1" }, 2, "Issue");
            _resultProcessingService.AddMessageProcessingResult(successResult);
            _resultProcessingService.AddMessageProcessingResult(errorResult);

            var allMessages = _resultProcessingService.GetAllProcessedMessages();

            Assert.Equal(2, allMessages.Count);
            Assert.Contains(successResult, allMessages);
            Assert.Contains(errorResult, allMessages);
        }

        [Fact]
        public void GetFailedMessages_ReturnsOnlyErrorResultsBuiltWithErrorResultMethod()
        {
            var successResult = MessageProcessingResult.BuildSuccessResult(new Message { Content = "Success" }, 1);
            var errorResult1 = MessageProcessingResult.BuildErrorResult(new Message { Content = "Fail1" }, 4, "Problem 1");
            var errorResult2 = MessageProcessingResult.BuildErrorResult(new Message { Content = "Fail2" }, 6, "Problem 2");
            _resultProcessingService.AddMessageProcessingResult(successResult);
            _resultProcessingService.AddMessageProcessingResult(errorResult1);
            _resultProcessingService.AddMessageProcessingResult(errorResult2);

            var failedMessages = _resultProcessingService.GetFailedMessages();

            Assert.Equal(2, failedMessages.Count);
            Assert.Contains(errorResult1, failedMessages);
            Assert.Contains(errorResult2, failedMessages);
            Assert.DoesNotContain(successResult, failedMessages);
            Assert.All(failedMessages, result => Assert.Equal(Status.Error, result.Status));
        }

        [Fact]
        public void GetSuccessMessage_ReturnsOnlySuccessResultsBuiltWithSuccessResultMethod()
        {
            var successResult1 = MessageProcessingResult.BuildSuccessResult(new Message { Content = "Good1" }, 7);
            var successResult2 = MessageProcessingResult.BuildSuccessResult(new Message { Content = "Good2" }, 2);
            var errorResult = MessageProcessingResult.BuildErrorResult(new Message { Content = "Bad" }, 5, "Trouble");
            _resultProcessingService.AddMessageProcessingResult(successResult1);
            _resultProcessingService.AddMessageProcessingResult(successResult2);
            _resultProcessingService.AddMessageProcessingResult(errorResult);

            var successMessages = _resultProcessingService.GetSuccessMessage();

            Assert.Equal(2, successMessages.Count);
            Assert.Contains(successResult1, successMessages);
            Assert.Contains(successResult2, successMessages);
            Assert.DoesNotContain(errorResult, successMessages);
            Assert.All(successMessages, result => Assert.Equal(Status.Success, result.Status));
        }

        [Fact]
        public void GetFailedMessages_ReturnsEmptyListWhenNoErrors()
        {
            var successResult = MessageProcessingResult.BuildSuccessResult(new Message { Content = "Okay" }, 8);
            _resultProcessingService.AddMessageProcessingResult(successResult);

            var failedMessages = _resultProcessingService.GetFailedMessages();

            Assert.Empty(failedMessages);
        }

        [Fact]
        public void GetSuccessMessage_ReturnsEmptyListWhenNoSuccesses()
        {
            var errorResult = MessageProcessingResult.BuildErrorResult(new Message { Content = "Trouble" }, 9, "Big Problem");
            _resultProcessingService.AddMessageProcessingResult(errorResult);

            var successMessages = _resultProcessingService.GetSuccessMessage();

            Assert.Empty(successMessages);
        }
    }
}