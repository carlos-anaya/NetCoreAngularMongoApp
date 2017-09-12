using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsStore.Repository;

namespace ProductsStore.Apis
{
    [Route("api/states")]
    public class StatesController : Controller
    {
        private IStatesRepository _statesRepository;
        private ILogger _logger;

        public StatesController(IStatesRepository statesRepository, ILoggerFactory loggerFactory)
        {
            _statesRepository = statesRepository;
            _logger = loggerFactory.CreateLogger(nameof(StatesController));
        }
    }
}