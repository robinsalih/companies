namespace Companies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{

    [HttpGet(Name = "GetCompanies")]
    public Task<List<Company>> GetCompanies()
    {
        return companyService.GetAll();
    }
}