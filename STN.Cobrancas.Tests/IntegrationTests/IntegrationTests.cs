using FluentAssertions;
using Newtonsoft.Json;
using STN.Cobrancas.Data.Models;
using STN.Cobrancas.Tests.Fixtures;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace STN.Cobrancas.Tests.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly TestContext _testContext;

        public IntegrationTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Values_GetCpfMes_ValuesReturnsOkResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/v1/Cobranca?cpf=63955700054&mes=11");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Values_GetCpfMes_Cpf_ValuesReturnsOkResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/v1/Cobranca?cpf=63955700054");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Values_GetCpfMes_Mes_ValuesReturnsOkResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/v1/Cobranca?mes=11");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Values_GetCpfMes_ReturnsBadRequestResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/v1/Cobranca/");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Values_GetCpfMes_Cpf_ReturnsBadRequestResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/v1/Cobranca/?cpf=639557000XX&mes=11");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Values_GetCpfMes_Mes_ReturnsBadRequestResponse()
        {
            var response = await _testContext.Client.GetAsync("/api/v1/Cobranca/?cpf=63955700054&mes=15");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Values_Post_ValuesReturnsOkResponse()
        {
            var cobranca = new Cobranca() { Cpf = "63955700054" };
            var serializedCob = JsonConvert.SerializeObject(cobranca);
            var content = new StringContent(serializedCob, Encoding.UTF8, "application/json");
            var response = await _testContext.Client.PostAsync("api/v1/Cobranca", content);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Values_Post_ReturnsBadRequestResponse()
        {
            var cobranca = new Cobranca() { Cpf = "4565" };
            var serializedCob = JsonConvert.SerializeObject(cobranca);
            var content = new StringContent(serializedCob, Encoding.UTF8, "application/json");
            var response = await _testContext.Client.PostAsync("api/v1/Cobranca", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
