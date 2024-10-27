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
        public CustomerController(CustomerRepository customerRepository, ILogger<CustomerController> logger, ActivitySource activitySource) 
        {
        }

        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        public IActionResult Get(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Post([FromBody] CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public IActionResult Put(Guid customerId, [FromBody] Customer customer)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
