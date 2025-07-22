using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsCommand, ProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductsResult> Handle(ListProductsCommand request, CancellationToken cancellationToken)
    {
        var paginationData = await _productRepository.ListProductsAsync(request, cancellationToken);

        return _mapper.Map<ProductsResult>(paginationData);
    }
}
