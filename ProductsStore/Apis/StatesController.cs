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
    [Route("api/states")]
    public class StatesController : Controller
    {
        private readonly IStatesRepository _statesRepository;
        private readonly ILogger _logger;

        public StatesController(IStatesRepository statesRepository, ILoggerFactory loggerFactory)
        {
            _statesRepository = statesRepository;
            _logger = loggerFactory.CreateLogger(nameof(StatesController));
        }

        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<State>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                var states = await _statesRepository.GetStatesAsync();
                return Ok(states);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}