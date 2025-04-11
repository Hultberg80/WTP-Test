using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using server.Services;
using Xunit;

namespace XUnitTest
{
    public class EmailServiceTests
    {
        private readonly EmailService _emailService;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<EmailService>> _loggerMock;

        public EmailServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<EmailService>>();

            // Lägg till dummydata för konfig-värden
            _configurationMock.Setup(c => c["Email:SmtpServer"]).Returns("smtp.test.com");
            _configurationMock.Setup(c => c["Email:Port"]).Returns("587");
            _configurationMock.Setup(c => c["Email:Username"]).Returns("user@test.com");
            _configurationMock.Setup(c => c["Email:Password"]).Returns("password123");
            _configurationMock.Setup(c => c["Email:From"]).Returns("noreply@test.com");

            _emailService = new EmailService(_configurationMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task SendChatInvitation_ShouldReturnTrue_WithValidConfiguration()
        {
            // Act
            var result = await _emailService.SendChatInvitation(
                "recipient@example.com",
                "https://chat.example.com/abc123",
                "Martin");

            // Assert
            Assert.True(result); // OBS! Fungerar endast om SMTP-servern finns (eller mockas vidare)
        }

        [Fact]
        public async Task SendChatInvitation_ShouldReturnFalse_WhenConfigurationMissing()
        {
            // Arrange – ingen konfig sätts
            var config = new Mock<IConfiguration>();
            var service = new EmailService(config.Object, _loggerMock.Object);

            // Act
            var result = await service.SendChatInvitation(
                "recipient@example.com",
                "https://chat.example.com/abc123",
                "Martin");

            // Assert
            Assert.False(result);
        }
    }
}
