namespace Companies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpGet()]
    public Task<List<Company>> GetCompanies() => companyService.GetAll();

    [HttpPost]
    public async Task<IActionResult> New(NewCompanyRequest request)
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Exchange = request.Exchange,
            Ticker = request.Ticker,
            Isin = request.Isin,
        };

        var result = await companyService.SaveCompany(company);

        return result.Success
            ? Ok(company.Id)
            : BadRequest(result.ErrorMessage);
    }
}