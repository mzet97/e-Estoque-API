using e_Estoque_API.Application.Customers.Commands;
using e_Estoque_API.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_Estoque_API.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : MainController
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SearchCustomerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result == null)
            return CustomResponse(false, null);

        return CustomResponse(true, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetByIdCustomerQuery(id);

        var result = await _mediator.Send(query);

        if (result == null)
            return CustomResponse(false, null);

        return CustomResponse(true, result);
    }

    [Authorize(Roles = "Create")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
    {
        var id = await _mediator.Send(command);

        return await Get(id);
    }

    [Authorize(Roles = "Create")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] UpdateCustomerCommand command)
    {
        var result = await _mediator.Send(command);

        return await Get(result);
    }

    [Authorize(Roles = "Create")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCustomerCommand(id);

        var result = await _mediator.Send(command);

        return CustomResponse(true, result);
    }
}