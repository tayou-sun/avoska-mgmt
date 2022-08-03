using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AvoskaMgmt.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public OrderController(ILogger<WeatherForecastController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }


        [HttpGet]
        public IEnumerable<OrderProductTagDto> Get(int id)
        {
            var rng = new Random();
            return _orderRepository.Get(id).ToList();
        }

        [HttpGet("status")]
        public StatusDto GetCurrentStatus(int id)
        {
            var rng = new Random();
            return _orderRepository.GetStatus(id);
        }

        [HttpPost("status")]
        public StatusDto SetNextStatus(Order order)
        {
            var rng = new Random();
            return _orderRepository.SetStatus(order.Id);
        }

    }
}
