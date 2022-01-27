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
    public class ResultController : ControllerBase
    {
         private readonly IResultRepository _resultRepository;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public ResultController(ILogger<WeatherForecastController> logger, IResultRepository resultRepository)
        {
            _logger = logger;
             _resultRepository = resultRepository;
        }

        [HttpGet]
        public IEnumerable<ResultDto> Get()
        {
            return _resultRepository.Get().ToList();
        }


        
        [HttpPost]
        public void Create([FromBody] ResultSaveDto res)
        {
            var a = new ResultSaveDto();
           _resultRepository.Create(res);
        }
    }
}
