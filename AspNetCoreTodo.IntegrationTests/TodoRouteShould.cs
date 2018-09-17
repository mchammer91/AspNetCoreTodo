using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TodoRouteShould : IClassFixture<TestFixure>
    {
        private readonly HttpClient httpClient;

        public TodoRouteShould(TestFixure fixture)
        {
            httpClient = fixture.Client;
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/todo");

            var response = await httpClient.SendAsync(request);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal(@"http://localhost:8888/Account/Login?ReturnUrl=%2Ftodo", response.Headers.Location.ToString());
        }
    }
}
