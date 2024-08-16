using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {

        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }





    }
}
