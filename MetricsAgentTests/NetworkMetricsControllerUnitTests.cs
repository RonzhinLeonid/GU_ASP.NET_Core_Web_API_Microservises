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
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> mockLogger;
        private IMapper mapper;

        public NetworkMetricsControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(mock.Object, mockLogger.Object, mapper);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetMetrics(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
