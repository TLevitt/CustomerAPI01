using CustomerAPI01.Data;
using CustomerAPI01.Data.DTO;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerAPI01.Test.UnitTests
{
    public class CrudTests
    {
        private CustomerRepository _customerRepository;

        public CrudTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
            optionsBuilder.UseInMemoryDatabase("InMemoryCustomerDB");

            var customerDbContext = new CustomerDbContext(optionsBuilder.Options);

            customerDbContext.Database.EnsureCreated();

            TestData.Current = new TestData(customerDbContext);
            TestData.Current.InitializeDbForTests();

            var logger = new TestConsoleLogger<CustomerRepository>();

            _customerRepository = new CustomerRepository(customerDbContext, logger);
        }

        [Fact]
        public void CreateCustomerTest()
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

            var customer = _customerRepository.AddCustomer(newCustomer);

            customer.CustomerId.Should().NotBe(Guid.Empty);
            //more tests
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            var customers = _customerRepository.GetCustomers();
            customers.Should().NotBeEmpty();
            customers.Count().Should().BeGreaterThan(1);
            //more tests
        }

        [Fact]
        public async Task GetCustomerTest()
        {
            var customers = _customerRepository.GetCustomers();
            var customer = customers.FirstOrDefault();
            var retrievedCustomer = _customerRepository.GetCustomer(customer.CustomerId);

            retrievedCustomer.CustomerId.Should().Be(customer.CustomerId);
            //more tests
        }

        [Fact]
        public async Task UpdateCustomerTest()
        {
            var customers = _customerRepository.GetCustomers();
            var customer = customers.FirstOrDefault();
            customer.FirstName = "Updated";

            _customerRepository.UpdateCustomer(customer);

            var updatedCustomer = _customerRepository.GetCustomer(customer.CustomerId);
            updatedCustomer.FirstName.Should().Be("Updated");

            //more tests
        }

        [Fact]
        public async Task DeleteCustomerTest()
        {
            var customers = _customerRepository.GetCustomers();
            var customer = customers.FirstOrDefault();
            _customerRepository.DeleteCustomer(customer.CustomerId);
            var deletedCustomer = _customerRepository.GetCustomer(customer.CustomerId);
            deletedCustomer.Should().BeNull();
            //more tests
        }

    }
}
