using e_Estoque_API.Application.Companies.Commands;
using e_Estoque_API.Application.Companies.Queries;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace e_Estoque_API.API.Controllers.OData;

[Authorize]
[Route("odata/[controller]")]
public class CompaniesController(ICompanyRepository companyRepository) : ODataController
{

    [HttpGet]
    [HttpGet("$count")]
    [EnableQuery(MaxExpansionDepth = 10)]
    public IQueryable<Company> Get()
       => companyRepository.GetAllQueryable();

    [HttpGet("({key})")]
    [EnableQuery]
    public SingleResult<Company> Get([FromODataUri] Guid key)
    {
        var result = companyRepository.GetAllQueryable().Where(c => c.Id == key);
        return SingleResult.Create(result);
    }
}
