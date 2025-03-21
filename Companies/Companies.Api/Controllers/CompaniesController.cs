using Companies.Core.Interfaces;
using Companies.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompaniesController(ICompanyRepository repository) : ControllerBase
{

    [HttpGet(Name = "GetCompanies")]
    public Task<List<Company>> GetCompanies()
    {
        return repository.GetAll();
    }
}