using AutoMapper;
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

        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> GetCustomersPaged(int skip, int take)
        {
            try
            {
                var pagedResult = await _customersRepository.GetCustomersPagedAsync(skip, take);
                Response.Headers.Add("X-Total-Records", pagedResult.TotalRecords.ToString());
                return Ok(pagedResult.Records);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        [NoCache]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetCustomer(string id)
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

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> AddCustomer([FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse { Status = false, ModelStateDictionary = ModelState.GetErrors() });
            try
            {
                var newCustomer = await _customersRepository.InsertCustomerAsync(Mapper.Map<Customer>(customer));
                if (newCustomer == null)
                    return BadRequest(new ApiResponse { Status = false });
                return CreatedAtRoute("GetCustomer", new { id = newCustomer.Id },
                    new ApiResponse() { Customer = newCustomer, Status = true });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateCustomer(string id, [FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse { Status = false, ModelStateDictionary = ModelState.GetErrors() });
            try
            {
                var customerToRepo = Mapper.Map<Customer>(customer);
                customerToRepo.Id = id;

                var status = await _customersRepository.UpdateCustomerAsync(customerToRepo);
                if (!status)
                    return BadRequest(new ApiResponse { Status = false });
                return Ok(new ApiResponse { Status = true, Customer = customerToRepo });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            var customer = await _customersRepository.GetCustomerAsync(id);
            if (customer == null)
                return NotFound();

            try
            {
                var status = await _customersRepository.DeleteCustomerAsync(customer);
                if (!status)
                    return BadRequest(new ApiResponse { Status = false });
                return Ok(new ApiResponse { Status = true });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}