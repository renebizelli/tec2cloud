using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a cart by user
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartByUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        var command = new GetCartByUserCommand()
        {
            UserId = Guid.NewGuid(),    
        };

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new GetCartByUserResponse());
    }
}
