using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[Authorize]
[ApiController]
[Route("api")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("branch/{branch}/[controller]")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromRoute] Guid branch, CancellationToken cancellationToken)
    {
        var request = new CreateSaleRequest
        {
            UserId = GetCurrentUserId(),
            BranchId = branch
        };

        var command = _mapper.Map<CreateSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<SaleResponse>(result);

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("[controller]/{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromRoute] Guid branch, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest
        {
            SaleId = id
        };

        var command = _mapper.Map<GetSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<SaleResponse>(result);

        return Ok(response);
    }

}
