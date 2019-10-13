using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameManager;

namespace broom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitGameController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<InitGameController> _logger;

        public InitGameController(ILogger<InitGameController> logger)
        {
            _logger = logger;
        }

        [EnableCors]
        [HttpGet]
        // public string[] Get()
        public GameState Get()
        {
            Game g = new Game();
            return g.InitGame(g, g.pl1, g.pl2, g.table_cards); 
            // return Summaries; 

        }

        [EnableCors]
        [HttpPost]
        // public string[] Get()
        public GameState Post()
        {
            Game g = new Game();
            return g.InitGame(g, g.pl1, g.pl2, g.table_cards); 
            // return Summaries; 

        }
    }
}
