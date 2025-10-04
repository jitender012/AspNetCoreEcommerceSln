using AutoMapper;
using eCommerce.Application.Features.MeasurementUnitFeature.Dtos;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.MeasurementUnitFeature.Queries
{
    public record GetMeasurementUnitsQuery : IRequest<List<MeasurementUnitDTO>>;
    public class GetMeasurementUnitsQueryHandler : IRequestHandler<GetMeasurementUnitsQuery, List<MeasurementUnitDTO>>
    {
        private readonly IMeasurementUnitRepository _measurementUnitRepository;
        private readonly IMapper _mapper;
        public GetMeasurementUnitsQueryHandler(IMeasurementUnitRepository measurementUnitRepository, IMapper mapper)
        {
            _measurementUnitRepository = measurementUnitRepository;
            _mapper = mapper;
        }
        public async Task<List<MeasurementUnitDTO>> Handle(GetMeasurementUnitsQuery request, CancellationToken cancellationToken)
        {
            var units = await _measurementUnitRepository.GetAllMeasurementUnitsAsync();
            var unitsDto = _mapper.Map<List<MeasurementUnitDTO>>(units);
            return unitsDto;
        }
    }
}
