using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListProductHandler : IRequestHandler<ListSalesCommand, PaginatedResult<SaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ListProductHandler> _logger;

    public ListProductHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<ListProductHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<SaleResult>> Handle(ListSalesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ListSales] Start");

        var paginationData = await _saleRepository.ListSalesAsync(request, cancellationToken);
        return _mapper.Map<PaginatedResult<SaleResult>>(paginationData);
    }
}
