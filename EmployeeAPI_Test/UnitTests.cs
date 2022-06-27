using Xunit;
using System;
using EmployeeAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace EmployeeAPI_Test
{
    public class UnitTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient httpClient;


        public UnitTests(WebApplicationFactory<Program> factory)
        {
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetEmployeeInformationTest()
        {

            var response = await httpClient.GetAsync("api/employee");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var employees = JsonSerializer.Deserialize<List<EmployeeInformation>>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            Assert.Equal(5, employees.Count);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetEmployeeByIdTest(int id)
        {

            Console.WriteLine("Hello", id);

            var response = await httpClient.GetAsync($"api/employee/{id}");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var employee = JsonSerializer.Deserialize<EmployeeInformation>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            Assert.Equal(id, employee.employee_id);
        }
    }
}