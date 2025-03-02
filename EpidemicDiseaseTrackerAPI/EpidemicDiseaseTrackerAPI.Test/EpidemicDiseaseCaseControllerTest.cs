using Moq;
using Microsoft.AspNetCore.Mvc;
using EpidemicDiseaseTrackerAPI.Controllers;
using EpidemicDiseaseTrackerAPI.Models;
using EpidemicDiseaseTrackerAPI.Repository;

namespace EpidemicDiseaseTrackerAPI.Tests
{
    public class DiseaseCasesControllerTests
    {
        private Mock<IEpidemicDiseaseCasesRepository> _mockService;
        private EpidemicDiseaseCasesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IEpidemicDiseaseCasesRepository>();

            _controller = new EpidemicDiseaseCasesController(_mockService.Object);
        }

        [Test]
        public async Task GetYearlyCases_ReturnsOk_WithData()
        {
            var mockData = new List<DiseaseCasesByYear>
            {
                new DiseaseCasesByYear { ReportYear = 2020, CasesReported = 500 },
                new DiseaseCasesByYear { ReportYear = 2021, CasesReported = 600 }
            };
            _mockService.Setup(s => s.GetYearlyCasesAsync()).ReturnsAsync(mockData);

            var result = await _controller.GetCasesByYear();

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var returnedData = okResult.Value as List<DiseaseCasesByYear>;
            Assert.That(returnedData.Count, Is.EqualTo(2));
            Assert.That(returnedData[0].ReportYear, Is.EqualTo(2020));
        }

        [Test]
        public async Task GetYearlyCases_ReturnsNotFound_WhenNoData()
        {
            _mockService.Setup(s => s.GetYearlyCasesAsync()).ReturnsAsync(new List<DiseaseCasesByYear>());

            var result = await _controller.GetCasesByYear();

            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(notFoundResult.Value, Is.EqualTo("No data available"));
        }
    }
}
