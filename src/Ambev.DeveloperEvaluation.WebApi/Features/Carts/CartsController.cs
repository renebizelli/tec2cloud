using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[Authorize]
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

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateOrUpdateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateOrUpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateOrUpdateCartCommand>(request);

        command.UserId = GetCurrentUserId();

        await _mediator.Send(command, cancellationToken);

        return Created();
    }

    [HttpGet()]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartByUserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        var command = new GetCartByUserCommand()
        {
            UserId = GetCurrentUserId()    
        };

        var response = await _mediator.Send(command, cancellationToken);

        var result = _mapper.Map<GetCartByUserResponse>(response);

        return Ok(result);
    }

    [HttpDelete()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCart(CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { UserId = GetCurrentUserId() };

        var command = _mapper.Map<DeleteCartCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return Accepted();
    }

}
