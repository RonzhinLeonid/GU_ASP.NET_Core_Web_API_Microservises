using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> mockLogger;

        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(mock.Object, mockLogger.Object);
        }


        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetAvailable(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetAvailable(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
