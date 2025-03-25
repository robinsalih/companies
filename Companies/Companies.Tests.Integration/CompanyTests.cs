namespace Companies.Tests.Integration;

public class CompanyTests : TestsBase
{
    private async Task<T> SendRequestAndDeserializeResponse<T>(HttpRequestMessage request)
    {
        var response = await TestClient.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<T>(responseContent);
        Assert.NotNull(deserialized);
        return deserialized;
    }

    private async Task SendRequestAndCheckResponseStatus(HttpRequestMessage request)
    {
        var response = await TestClient.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ShouldGetCompanies()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/Companies");
            
        var companies = await SendRequestAndDeserializeResponse<List<Company>>(request);

        Assert.NotEmpty(companies);
    }

    [Fact]
    public async Task ShouldSaveNewCompany()
    {
        var body = new CompanyRequest
        {
            Exchange = "LSE", Isin = "IE00BYTBXV33", Name = "Ryanair Holdings Plc", Ticker = "0RYA", Website = "https://ryanair.com"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "/Companies")
        {
            Content = JsonContent.Create(body)
        };

        var id = await SendRequestAndDeserializeResponse<Guid>(request);
        
        var created = await Helper.GetAndDeleteCompany(id);
        Assert.NotNull(created);
        Assert.Equal(body.Exchange, created.Exchange);
        Assert.Equal(body.Isin, created.Isin);
        Assert.Equal(body.Name, created.Name);
        Assert.Equal(body.Ticker, created.Ticker);
        Assert.Equal(body.Website, created.Website);
    }
}