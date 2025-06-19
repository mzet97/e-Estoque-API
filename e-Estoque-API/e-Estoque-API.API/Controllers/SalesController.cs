using e_Estoque_API.Application.Products.Commands;
using e_Estoque_API.Application.Sales.Commands;
using e_Estoque_API.Application.Sales.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_Estoque_API.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SalesController : MainController
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SearchSaleQuery query)
    {
        var result = await _mediator.Send(query);

        if (result == null)
            return CustomResponse(false, null);

        return CustomResponse(true, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetByIdSaleQuery(id);

        var result = await _mediator.Send(query);

        if (result == null)
            return CustomResponse(false, null);

        return CustomResponse(true, result);
    }

    [Authorize(Roles = "Create")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSaleCommand command)
    {
        var id = await _mediator.Send(command);

        return await Get(id);
    }

    [Authorize(Roles = "Create")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return await Get(result);
    }

    [Authorize(Roles = "Create")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteProductCommand(id);

        var result = await _mediator.Send(command);

        return CustomResponse(true, result);
    }
}
