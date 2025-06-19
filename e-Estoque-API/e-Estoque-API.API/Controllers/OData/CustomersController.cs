using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace e_Estoque_API.API.Controllers.OData;

[Authorize]
[Route("odata/[controller]")]
public class CustomersController(ICustomerRepository customerRepository) : ODataController
{

    [HttpGet]
    [HttpGet("$count")]
    [EnableQuery(MaxExpansionDepth = 10)]
    public IQueryable<Customer> Get()
       => customerRepository.GetAllQueryable();

    [HttpGet("({key})")]
    [EnableQuery]
    public SingleResult<Customer> Get([FromODataUri] Guid key)
    {
        var result = customerRepository.GetAllQueryable().Where(c => c.Id == key);
        return SingleResult.Create(result);
    }
}