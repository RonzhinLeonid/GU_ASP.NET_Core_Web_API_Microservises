using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class CpuControllerUnitTests
    {
        //private CpuMetricsController controller;
        //private Mock<ILogger<CpuMetricsController>> mockLogger;
        //public CpuControllerUnitTests()
        //{
        //    mockLogger = new Mock<ILogger<CpuMetricsController>>();
        //    controller = new CpuMetricsController(mockLogger.Object);
        //}

        //[Fact]
        //public void GetMetricsFromAgent_ReturnsOk()
        //{
        //    var agentId = 1;
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);

        //    var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}

        //[Fact]
        //public void GetMetricsFromAllCluster_ReturnsOk()
        //{
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);

        //    var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
    }
}
