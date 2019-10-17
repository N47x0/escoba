using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameManager;
using Business.Services;
using Business.Models;

namespace broom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayRoundController : ControllerBase
    {
        private readonly ILogger<PlayRoundController> _logger;

        public PlayRoundController(ILogger<PlayRoundController> logger)
        {
            _logger = logger;
        }

        Dictionary<int, ClientSession> ClientSessionDict = new Dictionary<int, ClientSession>();

        [EnableCors]
        [HttpGet]
        // public string[] Get()
        public ClientSessionPayload Get()
        {
            Game g = new Game();
            ClientSession cs = new ClientSession {
                _Game = g,
                Player1 = g.m_pl1,
                Player2 = g.m_pl2
            };

            ClientSessionPayload payload = new ClientSessionPayload {
                Id = cs.NewId(),
                _GameState = g.InitGame(g, g.m_pl1, g.m_pl2, g.m_table_cards)
            };
            ClientSessionDict.Add(cs.Id, cs);
            Console.WriteLine("Client Session Id: {0}", cs.Id);
            return payload; 

        }

        [EnableCors]
        [HttpPost]
        // public string[] Get()
        public ClientSessionPayload Post()
        {
            Game g = new Game();
            ClientSession cs = new ClientSession {
                _Game = g,
                Player1 = g.m_pl1,
                Player2 = g.m_pl2
            };

            ClientSessionPayload payload = new ClientSessionPayload {
                Id = cs.Id,
                _GameState = g.InitGame(g, g.m_pl1, g.m_pl2, g.m_table_cards)
            };

            return payload; 

        }
    }
}


    // if (g.deck.deck_order.Count == 0) {

    // }
