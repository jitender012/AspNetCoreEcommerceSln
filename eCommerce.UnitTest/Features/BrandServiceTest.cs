using AutoFixture;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.BrandFeature.Handlers;
using eCommerce.Application.Features.BrandFeature.Validators;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.UnitTest.Features
{
    public class BrandServiceTest
    {    
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
        }

        #region AddBrand        
        [Fact]
        public async Task AddBrandAsync_ShouldReturnBrandId_WhenBrandIsValid()
        {
            var userId = Guid.NewGuid();
            // Arrange
            var brandDto = _fixture.Build<CreateBrandDto>()
                .Create();

            var expectedId = Guid.NewGuid();

            var insertedBrand = new Brand { BrandId = expectedId, BrandName = "Test Brand" };

            _brandRepositoryMock
                .Setup(r => r.InsertAsync(It.IsAny<Brand>()))
                .ReturnsAsync(insertedBrand);

            _userContextServiceMock
                .Setup(u => u.GetUserId())
                .Returns(userId);

            var handler = new CreateBrandHandler(_brandRepositoryMock.Object, _userContextServiceMock.Object);
            var command = new CreateBrandCommand(brandDto);                

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedId, result);
        }

        [Fact]
        public void CreateBrandCommand_ShouldFail_WhenBrandNameIsEmpty()
        {
            // Arrange
            var brandDto = _fixture.Build<CreateBrandDto>()
                .With(b => b.BrandName, "")                
                .Create();

            var validator = new CreateBrandValidator();
            var command = new CreateBrandCommand(brandDto); 

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Dto.BrandName");
        }
        #endregion

        #region UpdateBrand
        #endregion

        #region DeleteBrandAsync    
        #endregion
    }
}
