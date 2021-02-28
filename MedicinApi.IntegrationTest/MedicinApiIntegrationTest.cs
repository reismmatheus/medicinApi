using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MedicinApi.IntegrationTest
{
    public class MedicinApiIntegrationTest
    {
        [Fact]
        public async Task Test_GetAll()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/medico");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("clínico geral")]
        public async Task Test_GetByEspecialidade(string especialidade)
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/medico/{especialidade}");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("José", "926.752.270-13", "12345-67", "Clínico Geral, Dermatologia", "Admin", "1234")]
        public async Task Test_InsertMedico(string nome, string cpf, string crm, string especialidades, string login, string password)
        {
            var medico = new Models.Medico { Nome = nome, Cpf = cpf, Crm = crm, Especialidades = especialidades.Split(",").ToList() };
            using (var client = new TestClientProvider().Client)
            {
                var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVmMDQ5YWFlLTM3YTUtNDRkMC04ODJiLTA4ZDhkYjY1ZjE3NyIsIm5iZiI6MTYxNDU0OTEzOSwiZXhwIjoxNjE1MTUzOTM5LCJpYXQiOjE2MTQ1NDkxMzl9.j46JpM5C4aYU-4lGpxww8RKTI71UJumBSBli0x6JR-Q";
                //await Login(login, password);

                if (string.IsNullOrEmpty(token))
                    Assert.Throws<ArgumentException>(() => { });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(medico);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/medico", content);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        private async Task<string> Login(string login, string password)
        {
            using (var client = new TestClientProvider().Client)
            {
                var json = JsonSerializer.Serialize(new { Username = login, Password = password });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var auth = await client.PostAsync($"/api/user/auth", content);

                if(auth.StatusCode == HttpStatusCode.BadRequest)
                {
                    return "";
                }

                var responseString = await auth.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<JObject>(responseString);

                return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVmMDQ5YWFlLTM3YTUtNDRkMC04ODJiLTA4ZDhkYjY1ZjE3NyIsIm5iZiI6MTYxNDU0OTEzOSwiZXhwIjoxNjE1MTUzOTM5LCJpYXQiOjE2MTQ1NDkxMzl9.j46JpM5C4aYU-4lGpxww8RKTI71UJumBSBli0x6JR-Q";
            }
        }
    }
}
