namespace Companies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    public Task<List<Company>> GetCompanies() => companyService.GetAll();

    [HttpPost(Name="company")]
    public async Task<ActionResult> New(CompanyRequest request)
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

    [HttpPut("id:Guid")]
    public async Task<ActionResult> Update(Guid id, [FromBody] CompanyRequest request)
    {
        if (await companyService.GetById(id) == null)
            return NotFound();

        var company = new Company
        {
            Id = id,
            Name = request.Name,
            Exchange = request.Exchange,
            Ticker = request.Ticker,
            Isin = request.Isin,
        };

        var result = await companyService.UpdateCompany(company);

        return result.Success
            ? Ok(company.Id)
            : BadRequest(result.ErrorMessage);
    }
}