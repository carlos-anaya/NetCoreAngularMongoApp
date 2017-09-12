using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsStore.Repository;
using System.Threading.Tasks;

namespace ProductsStore.Apis
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private ICustomersRepository _customersRepository;
        private ILogger _logger;

        public CustomersController(ICustomersRepository customersRepository, ILoggerFactory loggerFactory)
        {
            _customersRepository = customersRepository;
            _logger = loggerFactory.CreateLogger(nameof(CustomersController));
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customersRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest();
            }
        }
    }
}