using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace e_Estoque_API.API.Controllers.OData;

[Authorize]
[Route("odata/Categories")]
public class CategoriesController(ICategoryRepository categoryRepository) : ODataController
{

    [HttpGet]
    [HttpGet("$count")]
    [EnableQuery(MaxExpansionDepth = 10)]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
       => await categoryRepository.GetAllQueryable().ToListAsync();

    [HttpGet("({key})")]
    [EnableQuery]
    public async Task<ActionResult<Category>> Get([FromODataUri] Guid key)
    {
        var category = await categoryRepository.GetAllQueryable().SingleOrDefaultAsync(c => c.Id == key);
        if (category == null) return NotFound();
        return category;
    }
}