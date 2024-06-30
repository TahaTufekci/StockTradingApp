using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainScreenController : ControllerBase
    {
        private readonly IMainScreenService _mainScreenService;

        public MainScreenController(IMainScreenService mainScreenService)
        {
            _mainScreenService = mainScreenService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(MainScreenInfo))]
        public IActionResult GetMainScreenInfo()
        {
            var mainScreenInfo = _mainScreenService.GetMainScreenInfo();
            return Ok(mainScreenInfo);
        }
    }
}
