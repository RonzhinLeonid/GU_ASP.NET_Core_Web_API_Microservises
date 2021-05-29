using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsController>> mockLogger;
        private IMapper mapper;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(mock.Object, mockLogger.Object, mapper);
        }

        [Fact]
        public void GetByTimePeriod_ReturnsOk()
        {
            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetByTimePeriod(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetByTimePeriod(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
