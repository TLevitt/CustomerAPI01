using CustomerAPI01.Data;
using CustomerAPI01.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAPI01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository _customerRepository;
        private ILogger<CustomerController> _logger;
        private ActivitySource _activitySource;

        public CustomerController(CustomerRepository customerRepository, ILogger<CustomerController> logger, ActivitySource activitySource) 
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _activitySource = activitySource;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        [ProducesResponseType(typeof(Customer), 200)]
        public IActionResult Get()
        {
            return Ok(_customerRepository.GetCustomers());
        }

        // GET api/<CustomerController>/5
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid customerId)
        {
            var customer = _customerRepository.GetCustomer(customerId);
            if (customer != null)
            {
                return Ok(customer);
            }
            
            return NotFound("Customer not found");

        }

        // POST api/<CustomerController>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public IActionResult Post([FromBody] CustomerDto customer)
        {
            using (var activity = _activitySource.StartActivity("SaveCustomer"))
            {
                var newCustomer = _customerRepository.AddCustomer(customer);

                return Ok(newCustomer.CustomerId);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{customerId}")]
        public IActionResult Put(Guid customerId, [FromBody] Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);

            return Ok();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{customerId}")]
        public IActionResult Delete(Guid customerId)
        {
            _customerRepository.DeleteCustomer(customerId);

            return Ok();
        }
    }
}
