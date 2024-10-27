using CustomerAPI01.Data;
using CustomerAPI01.Data.DTO;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;
using Assert = Xunit.Assert;

namespace CustomerAPI01.Test.IntegrationTests
{
    public class CustomerAPITest(CustomerAPIFactory<Program> factory) : IClassFixture<CustomerAPIFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task CreateCustomerTest()
        {
            var newCustomer = new CustomerDto()
            {
                EmailAddress = "test@testing.com",
                FirstName = "Test",
                LastName = "Testing",
                MiddleName = "Tested",
            };

            var phoneNumber = new PhoneNumberDto()
            {
                Number = "1234567890",
                Type = "Mobile",
            };

            newCustomer.PhoneNumbers = new List<PhoneNumberDto> { phoneNumber };

            var jsonContent = JsonSerializer.Serialize(newCustomer);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Customer", content);
            if (response.IsSuccessStatusCode)
            {
                response.Should().NotBeNull();
                var createResponse = await response.Content.ReadAsStringAsync();
                createResponse.Should().NotBeNull();
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Assert.Fail("Api call failed.");
            }
        }

        [Fact]
        public async Task ReadCustomerTest()
        {
            var testCustomer = TestData.Current.CustomerDbContext.Customers.First();

            var response = await _client.GetFromJsonAsync<Customer>($"/api/Customer/{testCustomer.CustomerId}");
            response.Should().NotBeNull();
            //more tests
        }

        [Fact]
        public async Task UpdateCustomerTest()
        {
#warning ToDo: Implement UpdateCustomerTest
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteCustomerTest()
        {
#warning ToDo: Implement DeleteCustomerTest        
            Assert.True(false);
        }

        [Fact]
        public async Task ValidationTest()
        {
            var newCustomer = new Customer()
            {
                EmailAddress = "test@testing.com",
                FirstName = "Test",
                LastName = "Testing",
                MiddleName = "Tested",
            };

            var jsonContent = JsonSerializer.Serialize(newCustomer);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Customer", content);
            if (response.IsSuccessStatusCode)
            {
                Assert.Fail("Api call should have failed validation check.");
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
                //check additional validation error messages
            }
        }
    }
}
