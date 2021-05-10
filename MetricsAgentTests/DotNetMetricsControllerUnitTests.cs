using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNetMetricsController>> mockLogger;
        private IMapper mapper;

        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mock.Object, mockLogger.Object, mapper);
        }

        [Fact]
        public void GetErrorsCount_ReturnsOk()
        {
            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetErrorsCount(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetErrorsCount(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
