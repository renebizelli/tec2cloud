using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all active products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var request = new GetProductsRequest();

        var command = _mapper.Map<GetProductsCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        var data = _mapper.Map<List<GetProductResponse>>(response);

        var paginatedList = new PaginatedList<GetProductResponse>(data, data.Count, 0, 1000);

        return OkPaginated(paginatedList);
    }
}
