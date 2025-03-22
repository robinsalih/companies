namespace Companies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    public Task<List<Company>> GetCompanies() => companyService.GetAll();

    [HttpPost(Name="company")]
    public async Task<ActionResult> New(NewCompanyRequest request)
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

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Company>> GetById(Guid id)
    {
        var result = await companyService.GetById(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{isin}")]
    public async Task<ActionResult<Company>> GetByIsin(string isin)
    {
        var result = await companyService.GetByIsin(isin);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}