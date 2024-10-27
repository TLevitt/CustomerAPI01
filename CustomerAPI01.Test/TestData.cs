using CustomerAPI01.Data;
using CustomerAPI01.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI01.Test
{
    public class TestData
    {
        public static TestData Current { get; set; }

        public CustomerDbContext CustomerDbContext { get; set; }

        public TestData(CustomerDbContext db) 
        { 
            CustomerDbContext = db;
        }
        public List<CustomerDto> GetSeedData()
        {
            var customers = new List<CustomerDto>();

            // Create individual customers and phone numbers
            CustomerDto customer1 = new CustomerDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "jdoe@email.com",
                MiddleName = "",
                PhoneNumbers = new List<PhoneNumberDto>
                {
                    new PhoneNumberDto { Number = "1234567890", Type = "Mobile" },
                    new PhoneNumberDto { Number = "9876543210", Type = "Home",  }
                }
            };

            CustomerDto customer2 = new CustomerDto
            {
                FirstName = "Jane",
                LastName = "Smith",
                EmailAddress = "jsmith@email.com",
                MiddleName = "",
                PhoneNumbers = new List<PhoneNumberDto>
                {
                    new PhoneNumberDto {Number = "5559876543", Type = "Mobile"},
                    new PhoneNumberDto { Number = "5559876543", Type = "Work", }
                }
            };

            CustomerDto customer3 = new CustomerDto
            {
                FirstName = "Bob",
                LastName = "Johnson",
                EmailAddress = "bjohnson@email.com",
                MiddleName = "",
                PhoneNumbers = new List<PhoneNumberDto>
            {
                new PhoneNumberDto {Number = "5559876543", Type = "Mobile"},
                new PhoneNumberDto {Number = "5559876543", Type = "Home"}
            }
            };

            // Add customers to the list
            customers.Add(customer1);
            customers.Add(customer2);
            customers.Add(customer3);

            return customers;
        }

        public void InitializeDbForTests()
        {
            CustomerDbContext.Customers.AddRange(GetSeedData().Select(c => new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = c.FirstName,
                LastName = c.LastName,
                EmailAddress = c.EmailAddress,
                MiddleName= c.MiddleName,
                PhoneNumbers = c.PhoneNumbers.Select(p => new PhoneNumber
                {
                    Number = p.Number,
                    Type = p.Type
                }).ToList()
            }));

            CustomerDbContext.SaveChanges();
        }
    }
}
