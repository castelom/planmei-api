using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Planmei.Web.Controllers;
using Planmei.Domain.Interfaces.Services;
using Planmei.Domain.Models.Request;
using Planmei.Domain.Models.Response;

namespace Planmei.Test
{
    public class ContactControllerTest
    {
        [Fact]
        public async Task SendMessage_ReturnsOk_WhenCaptchaValidAndEmailSent()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ContactController>>();
            var captchaServiceMock = new Mock<ICaptchaService>();
            var emailServiceMock = new Mock<IEmailService>();

            captchaServiceMock
                .Setup(x => x.VerifyTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new CaptchaResponse { Success = true });

            emailServiceMock
                .Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new ContactController(
                loggerMock.Object,
                captchaServiceMock.Object,
                emailServiceMock.Object
            );

            var request = new MessageRequest
            {
                Email = "test@email.com",
                Subject = "Test Subject",
                Message = "Test Body",
                RecaptchaToken = "valid-token"
            };

            // Act
            var result = await controller.SendMessage(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SendMessage_ReturnsBadRequest_WhenCaptchaInvalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ContactController>>();
            var captchaServiceMock = new Mock<ICaptchaService>();
            var emailServiceMock = new Mock<IEmailService>();

            captchaServiceMock
                .Setup(x => x.VerifyTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new CaptchaResponse { Success = false });

            var controller = new ContactController(
                loggerMock.Object,
                captchaServiceMock.Object,
                emailServiceMock.Object
            );

            var request = new MessageRequest
            {
                Email = "test@email.com",
                Subject = "Test Subject",
                Message = "Test Body",
                RecaptchaToken = "invalid-token"
            };

            // Act
            var result = await controller.SendMessage(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Captcha validation failed.", badRequestResult.Value);
        }

        [Fact]
        public async Task SendMessage_ReturnsBadRequest_OnException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ContactController>>();
            var captchaServiceMock = new Mock<ICaptchaService>();
            var emailServiceMock = new Mock<IEmailService>();

            captchaServiceMock
                .Setup(x => x.VerifyTokenAsync(It.IsAny<string>()))
                .ThrowsAsync(new System.Exception("Captcha error"));

            var controller = new ContactController(
                loggerMock.Object,
                captchaServiceMock.Object,
                emailServiceMock.Object
            );

            var request = new MessageRequest
            {
                Email = "test@email.com",
                Subject = "Test Subject",
                Message = "Test Body",
                RecaptchaToken = "any-token"
            };

            // Act
            var result = await controller.SendMessage(request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}