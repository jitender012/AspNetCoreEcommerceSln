using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Application.Common.Exceptions;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services.ProductServices
{
    public class FeatureOptionService : IFeatureOptionService
    {
        private readonly IFeatureOptionRepository _featureOptionRepository;
        private readonly ILogger<FeatureOptionService> _logger;
        public FeatureOptionService(IFeatureOptionRepository featureOptionRepository, ILogger<FeatureOptionService> logger)
        {
            _logger = logger;
            _featureOptionRepository = featureOptionRepository;
        }

        public async Task<int> AddAsync(FeatureOptionDTO data)
        {
            try
            {
                var domainData = data.ToDomain();
                var id = await _featureOptionRepository.InsertAsync(domainData);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding FeatureOption. Data: {data}", data);
                throw new ServiceException("Something went wrong while adding feature option.", ex);
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _featureOptionRepository.RemoveFeatureOptionAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting FeatureOption with id={id}", id);
                throw new ServiceException("Error while deleting FeatureOption", ex);                
            }
        }

        public async Task<List<FeatureOptionDTO>> GetAllAsync()
        {

            try
            {
                var featureOptions = await _featureOptionRepository.FetchAllAsync();
                return FeatureOptionDTO.ToDtoList(featureOptions);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<FeatureOptionDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(FeatureOptionDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
