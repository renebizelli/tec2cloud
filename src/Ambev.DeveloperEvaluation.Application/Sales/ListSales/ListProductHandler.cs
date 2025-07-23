using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListProductHandler : IRequestHandler<ListSalesCommand, PaginatedResult<SaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public ListProductHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<SaleResult>> Handle(ListSalesCommand request, CancellationToken cancellationToken)
    {
        var paginationData = await _saleRepository.ListSalesAsync(request, cancellationToken);
        return _mapper.Map<PaginatedResult<SaleResult>>(paginationData);
    }
}
