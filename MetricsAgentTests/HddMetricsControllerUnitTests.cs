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
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> mockLogger; 
        private IMapper mapper;

        public HddMetricsControllerUnitTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mock.Object, mockLogger.Object, mapper);
        }

        [Fact]
        public void GetFreeHDDSpace_ReturnsOk()
        {
            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetFreeHDDSpace(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var fromTime = new DateTimeOffset(new DateTime(2021, 5, 11));
            var toTime = new DateTimeOffset(new DateTime(2021, 5, 20));

            var result = controller.GetFreeHDDSpace(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
