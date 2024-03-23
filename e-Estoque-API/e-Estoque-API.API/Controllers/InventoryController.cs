using e_Estoque_API.Application.Inventories.Commands;
using e_Estoque_API.Application.Inventories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_Estoque_API.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InventoryController : MainController
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SearchInventoryQuery query)
    {
        var result = await _mediator.Send(query);

        if (result == null)
            return CustomResponse(false, null);

        return CustomResponse(true, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetByIdInventoryQuery(id);

        var result = await _mediator.Send(query);

        if (result == null)
            return CustomResponse(false, null);

        return CustomResponse(true, result);
    }

    [Authorize(Roles = "Create")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInventoryCommand command)
    {
        var id = await _mediator.Send(command);

        return await Get(id);
    }

    [Authorize(Roles = "Create")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateInventoryCommand command)
    {
        var result = await _mediator.Send(command);

        return await Get(result);
    }

    [Authorize(Roles = "Create")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteInventoryCommand(id);

        var result = await _mediator.Send(command);

        return CustomResponse(true, result);
    }
}