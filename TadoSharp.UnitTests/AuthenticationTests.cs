using Flurl.Http.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using TadoSharp;

namespace Tests
{
    public class AuthenticationTests
    {
        HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public async Task CallCorrectEndpointWithCorrectParameters()
        {
            _httpTest.RespondWithJson(new { });
            var client = new TadoClient("test@example.com", "test123");
            await client.Authenticate();

            _httpTest.ShouldHaveCalled("https://auth.tado.com/oauth/token")
                .WithVerb(HttpMethod.Post)
                .WithRequestUrlEncoded(new { client_id = "tado-webapp", grant_type = "password", scope = "home.user", username = "test@example.com", password = "test123" })
                .Times(1);
        }
    }
}