using CustomerAPI01.Data.DTO;
using Microsoft.Extensions.Logging;

namespace CustomerAPI01.Data
{
    public class CustomerRepository
    {
        private CustomerDbContext _customerDbContext;
        private ILogger<CustomerRepository> _logger;

        public CustomerRepository(CustomerDbContext customerDbContext, ILogger<CustomerRepository> logger)
        {
            _customerDbContext = customerDbContext;
            _logger = logger;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            _logger.LogInformation("Retrieving customers");
            return _customerDbContext.Customers;
        }

        public Customer GetCustomer(Guid id)
        {
            _logger.LogInformation("Retrieving customer {id}", id);
            return _customerDbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public Customer AddCustomer(CustomerDto customer)
        {
            _logger.LogInformation("Adding new customer {customer}", System.Text.Json.JsonSerializer.Serialize(customer));
            var newCustomer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress
            };

            _customerDbContext.Customers.Add(newCustomer);

            if (customer.PhoneNumbers != null)
            {
                foreach (var phoneNumber in customer.PhoneNumbers)
                {
                    var newPhoneNumber = new PhoneNumber
                    {
                        Number = phoneNumber.Number,
                        Type = phoneNumber.Type,
                        CustomerId = newCustomer.CustomerId
                    };

                    _customerDbContext.PhoneNumbers.Add(newPhoneNumber);
                }
            }
            _customerDbContext.SaveChanges();

            return newCustomer;
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerDbContext.Customers.Update(customer);
            _customerDbContext.SaveChanges();
        }

        public void DeleteCustomer(Guid id)
        {
            var customer = _customerDbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                _customerDbContext.Customers.Remove(customer);
                _customerDbContext.SaveChanges();
            }
        }
    }
}
