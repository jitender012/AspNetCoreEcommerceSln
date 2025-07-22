using AutoFixture;
using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.UnitTest
{
    public class BrandServiceTest
    {
        private readonly BrandService _brandService;

        private readonly Mock<IBrandRepository> _brandRepositoryMock;
        private readonly Mock<IUserContextService> _mockUserContext;
        private readonly Mock<ILogger<BrandService>> _mockLogger;

        private readonly Fixture _fixture;

        public BrandServiceTest()
        {
            _mockUserContext = new Mock<IUserContextService>();
            _mockLogger = new Mock<ILogger<BrandService>>();

            _brandRepositoryMock = new Mock<IBrandRepository>();
            _brandService = new BrandService(_brandRepositoryMock.Object, _mockUserContext.Object, _mockLogger.Object);
            _fixture = new Fixture();
            
        }
        #region AddBrand
        
        [Fact]
        public async Task AddBrand_NullBrand()
        {
            //Arrange                       
            var brandDTO = _fixture.Build<BrandDTO>()
                          .Without(b => b.BrandName) 
                          .Create();


            //Act & Assert
            await FluentActions
                .Awaiting(() => _brandService.AddBrand(brandDTO))
                .Should()
                .ThrowAsync<ArgumentNullException>();          
        }



        //When the CountryName is duplicate, it should throw ArgumentExceptions
        [Fact]
        public void AddBrand_DuplicateBrandName()
        {
            //Arrange
            var brandDTO = _fixture.Build<BrandDTO>()
                .With(b => b.BrandName, "BrandName")
                .Create();

            var brandEntity = _fixture.Build<Brand>()
                .With(b => b.BrandName, "BrandName")
                .Create();

            //_brandRepositoryMock
            //   .Setup(repo => repo.ExistsByNameAsync(brandDTO.BrandName))
            //   .ReturnsAsync(true);

            //// Act
            //var action = async () => await _brandService.AddBrand(brandDTO);

            //// Assert
            //await action.Should().ThrowAsync<InvalidOperationException>()
            //    .WithMessage("Brand with the same name already exists");

            //_brandRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Brand>()), Times.Never);
        }

        ////When CountryAddRequest is null, it should throw ArgumentNullException
        //[Fact]
        //public async void AddBrand_PropernBrandDetails()
        //{
        //    //Arrange
        //    var brandDTO = new BrandDTO { BrandName = "Test", BrandDescription = "", BrandId = Guid.NewGuid(), BrandImage = "/etc/image/img.jpg" };
        //    var brand = new Brand { BrandName = "Test", BrandDescription = "", BrandId = Guid.NewGuid(), BrandImage = "/etc/image/img.jpg" };


        //    _brandRepositoryMock
        //    .Setup(repo => repo.InsertAsync(It.IsAny<Brand>()))
        //    .ReturnsAsync(brand);

        //    //Act
        //    var result = await _brandService.AddBrand(brandDTO);

        //    //Assert
        //    result.Should().NotBeEmpty();
        //    result..Should().Be(brandDTO.Name);

        //    _brandRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Brand>()), Times.Once);
        //}
        #endregion

    }
}
