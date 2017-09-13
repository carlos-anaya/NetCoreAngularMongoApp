using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsStore.Infraestructure;
using ProductsStore.Models;
using ProductsStore.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStore.Apis
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ILogger _logger;

        public CustomersController(ICustomersRepository customersRepository, ILoggerFactory loggerFactory)
        {
            _customersRepository = customersRepository;
            _logger = loggerFactory.CreateLogger(nameof(CustomersController));
        }

        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customersRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet("{id}")]
        [NoCache]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customersRepository.GetCustomerAsync(id);

                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}