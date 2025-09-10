using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Planmei.Domain.Interfaces.Services;
using Planmei.Domain.Models.Response;
using Planmei.Web.Controllers;

namespace Planmei.Test.Controllers
{
    public class FinancialControllerTest
    {
        private readonly Mock<ILogger<FinancialController>> _loggerMock;
        private readonly Mock<IFinancialTransactionService> _financialServiceMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly FinancialController _sut;

        public FinancialControllerTest()
        {
            _loggerMock = new Mock<ILogger<FinancialController>>();
            _financialServiceMock = new Mock<IFinancialTransactionService>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _sut = new FinancialController(
                _loggerMock.Object,
                _currentUserServiceMock.Object,
                _financialServiceMock.Object
            );
        }

        [Fact]
        public async Task Overview_ReturnsOk_WithStringValueAsync()
        {
            // Arrange
            _financialServiceMock
               .Setup(x => x.GetOverviewByMonthAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync(new OverviewResponse { Revenue = 1325, Expenses = 250, TargetAmount = 2000 });
            int testMonth = 5;

            // Act
            var result = await _sut.Overview(testMonth);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var overview = Assert.IsType<OverviewResponse>(okResult.Value);

            Assert.Equal(1325, overview.Revenue);
            Assert.Equal(250, overview.Expenses);
            Assert.Equal(2000, overview.TargetAmount);
        }

        [Fact]
        public async Task Overview_ReturnsBadRequest_OnExceptionAsync()
        {
            // Arrange
            _financialServiceMock
                .Setup(x => x.GetOverviewByMonthAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Error"));

            int testMonth = 5;

            // Act
            var result = await _sut.Overview(testMonth);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}