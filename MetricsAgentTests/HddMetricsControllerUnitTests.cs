using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;

        public HddMetricsControllerUnitTests()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetFreeHDDSpace_ReturnsOk()
        {
            var result = controller.GetFreeHDDSpace();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
