using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Auth.Dto;
using Microsoft.Extensions.Configuration;
using Moq;
using KBA.Domain.Entity.SellerAgg;
using RepresentativePanel.WebApi.Controllers.Auth;

namespace KermanBatterSeller.UnitTest.TestAuth
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthDataService> mockAuthDataService;
        private readonly Mock<ISellerRepository> mockSellerRepository;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<ISellerLoginService> mockSellerLoginService;
        private readonly AuthController controller;

        public AuthControllerTests()
        {
            mockAuthDataService = new Mock<IAuthDataService>();
            mockSellerRepository = new Mock<ISellerRepository>();
            mockConfiguration = new Mock<IConfiguration>();
            mockSellerLoginService = new Mock<ISellerLoginService>();

            controller = new AuthController(mockAuthDataService.Object, mockConfiguration.Object, mockSellerLoginService.Object);
        }
        [Fact]
        public async Task Login_ShouldReturn200_WhenValidCredentials()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                PhoneNumber = "09123456789",
                Password = "123456789", // valid password
                IPAddress = "192.168.1.1"
            };

            mockAuthDataService.Setup(s => s.Login(It.IsAny<LoginDto>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<TokenResultDto>
                {
                    ResultCode = 200,
                    Value = new TokenResultDto { Token = "validToken" }
                });

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var resultObject = Assert.IsType<Result<TokenResultDto>>(result);
            Assert.Equal(200, resultObject.ResultCode);
            Assert.Equal("validToken", resultObject.Value.Token);
        }



        [Fact]
        public async Task Login_ShouldReturn400_WhenInvalidCredentials()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                PhoneNumber = "09123456789",
                Password = "9874563210", // invalid password
                IPAddress = "192.168.1.1"
            };

            mockAuthDataService.Setup(s => s.Login(It.IsAny<LoginDto>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<TokenResultDto>
                {
                    ResultCode = 400,
                    ResultMessage = "ایمیل یا رمز عبور اشتباه است"
                });

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var resultObject = Assert.IsType<Result<TokenResultDto>>(result);
            Assert.Equal(400, resultObject.ResultCode);
            Assert.Equal("ایمیل یا رمز عبور اشتباه است", resultObject.ResultMessage);
        }
        [Fact]
        public async Task GetVerificationCode_ShouldReturn200_WhenValidPhoneNumber()
        {
            // Arrange
            var dto = new GetverificationCodeDto
            {
                PhoneNumber = "09123456789"
            };

            mockAuthDataService.Setup(s => s.GetVerificationCode(It.IsAny<GetverificationCodeDto>()))
                .ReturnsAsync(new Result<string>
                {
                    ResultCode = 200,
                    ResultMessage = "کد تأیید تولید شد",
                    Value = "123456"
                });

            // Act
            var result = await controller.GetVerificationCode(dto);

            // Assert
            var resultObject = Assert.IsType<Result<string>>(result);
            Assert.Equal(200, resultObject.ResultCode);
            Assert.Equal("کد تأیید تولید شد", resultObject.ResultMessage);
            Assert.Equal("123456", resultObject.Value);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturn200_WhenValidRequest()
        {
            // Arrange
            var dto = new ChangePasswordDto
            {
                PhoneNumber = "09123456789",
                NewPassword = "newPassword123",
                VerificationCode = "123456"
            };

            mockAuthDataService.Setup(s => s.ChangePassword(It.IsAny<ChangePasswordDto>()))
                .ReturnsAsync(new Result<string>
                {
                    ResultCode = 200,
                    ResultMessage = "رمز عبور با موفقیت تغییر یافت"
                });

            // Act
            var result = await controller.ChangePassword(dto);

            // Assert
            var resultObject = Assert.IsType<Result<string>>(result);
            Assert.Equal(200, resultObject.ResultCode);
            Assert.Equal("رمز عبور با موفقیت تغییر یافت", resultObject.ResultMessage);
        }
        [Fact]
        public async Task GetVerificationCode_ShouldReturn400_WhenInvalidPhoneNumber()
        {
            // Arrange
            var dto = new GetverificationCodeDto
            {
                PhoneNumber = "09123456789"
            };

            mockAuthDataService.Setup(s => s.GetVerificationCode(It.IsAny<GetverificationCodeDto>()))
                .ReturnsAsync(new Result<string>
                {
                    ResultCode = 400,
                    ResultMessage = "کاربری با این شماره تلفن یافت نشد"
                });

            // Act
            var result = await controller.GetVerificationCode(dto);

            // Assert
            var resultObject = Assert.IsType<Result<string>>(result);
            Assert.Equal(400, resultObject.ResultCode);
            Assert.Equal("کاربری با این شماره تلفن یافت نشد", resultObject.ResultMessage);
        }
        [Fact]
        public async Task ChangePassword_ShouldReturn400_WhenInvalidVerificationCode()
        {
            // Arrange
            var dto = new ChangePasswordDto
            {
                PhoneNumber = "09123456789",
                NewPassword = "newPassword123",
                VerificationCode = "654321" //Invalid
            };

            mockAuthDataService.Setup(s => s.ChangePassword(It.IsAny<ChangePasswordDto>()))
                .ReturnsAsync(new Result<string>
                {
                    ResultCode = 400,
                    ResultMessage = "کد تأیید نامعتبر یا منقضی شده است"
                });

            // Act
            var result = await controller.ChangePassword(dto);

            // Assert
            var resultObject = Assert.IsType<Result<string>>(result);
            Assert.Equal(400, resultObject.ResultCode);
            Assert.Equal("کد تأیید نامعتبر یا منقضی شده است", resultObject.ResultMessage);
        }
    }
}
