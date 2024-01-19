﻿using e_Estoque_API.Application.Categories.Commands;
using e_Estoque_API.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_Estoque_API.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : MainController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchCategoryQuery query)
        {
            var result = await _mediator.Send(query);

            if (result == null)
                return CustomResponse(false, null);

            return CustomResponse(true, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetByIdCategoryQuery(id);

            var result = await _mediator.Send(query);

            if (result == null)
                return CustomResponse(false, null);

            return CustomResponse(true, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            var id = await _mediator.Send(command);

            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return await Get(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand(id);

            var result = await _mediator.Send(command);

            return CustomResponse(true, result);
        }
    }
}
