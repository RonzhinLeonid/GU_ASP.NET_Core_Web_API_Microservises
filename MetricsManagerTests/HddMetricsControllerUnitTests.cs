﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> mockLogger;

        public HddMetricsControllerUnitTests()
        {
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mockLogger.Object);
        }

        [Fact]
        public void GetFreeHDDSpaceFromAgent_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.GetFreeHDDSpaceFromAgent(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetFreeHDDSpaceFromAllCluster_ReturnsOk()
        {
            var result = controller.GetFreeHDDSpaceFromAllCluster();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
