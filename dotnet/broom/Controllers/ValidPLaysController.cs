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
    public class ValidPlaysController : ControllerBase
    {

        public class ValidPlaysPayload {
            public List<Card> Hand { get; set; }
            public List<Card> TableCards { get; set; }
        }

        private readonly ILogger<ValidPlaysController> _logger;

        public ValidPlaysController(ILogger<ValidPlaysController> logger)
        {
            _logger = logger;
        }


        [EnableCors]
        [HttpPost ("FromBody")]
        // public string[] Get()
        public List<List<Card>> Post(ValidPlaysPayload input)
        {
            Console.WriteLine(input);
            Game g = new Game();
            List<List<Card>> output = new List<List<Card>>();
            output = Game.ValidPlays(input.Hand, input.TableCards);

            return output; 

        }
    }
}
