using System.Net;
using Companies.Api;
using Companies.Core.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Companies.Tests.Integration
{
    public class CompanyTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _testClient;

        public CompanyTests()
        {
                _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
                _testClient = _testServer.CreateClient();
        }
        
        ~CompanyTests()
        {
            _testServer.Dispose();
            _testClient.Dispose();
        }

        private async Task<T> SendRequestAndDeserializeResponse<T>(HttpRequestMessage request)
        {
            var response = await _testClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<T>(responseContent);
            Assert.NotNull(deserialized);
            return deserialized;
        }

        [Fact]
        public async Task ShouldGetCompanies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Companies");
            
            var companies = await SendRequestAndDeserializeResponse<List<Company>>(request);

            Assert.NotEmpty(companies);
        }
    }
}
