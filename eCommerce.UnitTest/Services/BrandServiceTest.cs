using AutoFixture;
using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;
using Moq;

namespace eCommerce.UnitTest.Services
{
    public class BrandServiceTest
    {
        private readonly IBrandService _brandService;

        private readonly Mock<IBrandRepository> _brandRepositoryMock;
        private readonly Mock<IUserContextService> _userContextServiceMock;
        private readonly Mock<ILogger<BrandService>> _loggerMock;

        private readonly IFixture _fixture;

        public BrandServiceTest()
        {
            _fixture = new Fixture();

            _loggerMock = new Mock<ILogger<BrandService>>();
            _userContextServiceMock = new Mock<IUserContextService>();
            _brandRepositoryMock = new Mock<IBrandRepository>();


            _brandService = new BrandService(
                _brandRepositoryMock.Object,
                _userContextServiceMock.Object,
                _loggerMock.Object
            );
        }

        #region AddBrand        
        [Fact]
        public async Task AddBrandAsync_ShouldReturnBrandId_WhenBrandIsValid()
        {
            // Arrange
            var brandDto = _fixture.Build<BrandDTO>()                
                .Create();

            var expectedId = Guid.NewGuid();

            var insertedBrand = new Brand { BrandId = expectedId, BrandName = "Test Brand" };

            _brandRepositoryMock
                .Setup(r => r.InsertAsync(It.IsAny<Brand>()))
                .ReturnsAsync(insertedBrand);

            _userContextServiceMock
                .Setup(u => u.GetUserId())
                .Returns(Guid.NewGuid);

            // Act
            var result = await _brandService.AddBrand(brandDto);

            // Assert
            Assert.Equal(expectedId, result);
        }

        [Fact]
        public async Task AddBrandAsync_ShouldThrowArgumentNullException_WhenBrandNameIsNull()
        {
            // Arrange
            var brandDto = _fixture.Build<BrandDTO>()
                .With(b => b.BrandName, "")
                .Create();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _brandService.AddBrand(brandDto));
        }
        #endregion

        #region UpdateBrand
        #endregion

        #region DeleteBrandAsync    
        #endregion
    }
}
