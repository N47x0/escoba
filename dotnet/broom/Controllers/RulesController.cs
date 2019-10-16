using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using GameManager;
using Npgsql;

namespace broom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RulesController : ControllerBase
    {

        private readonly ILogger<RulesController> _logger;

        public RulesController(ILogger<RulesController> logger)
        {
            _logger = logger;
        }

        Dictionary<int, ClientSession> ClientSessionDict = new Dictionary<int, ClientSession>();

        [EnableCors]
        [HttpGet]
        // public string[] Get()
        public IEnumerable<Object> Get()
        // public void Get()
        {
            var connString = "Host=localhost;Username=root;Password=password;Database=test";
            // var connString = "Host=localhost;Username=postgres;Password=password;Database=test";
            var optionsBuilder = new DbContextOptionsBuilder<BroomDbContext>();
            // optionsBuilder.UseNpgsql(connString);
            optionsBuilder.UseMySql(connString);
            using (var db = new BroomDbContext(optionsBuilder.Options))
            {
                var rules = db.Rules
                    .ToList();
                return rules;
            }

            // using (var connection = new NpgsqlConnection(connString))
            // {
            //     connection.Open();

            //     using (var cmd = new NpgsqlCommand("SELECT * FROM RULES", connection))                

            //     using (var reader = cmd.ExecuteReader())
            //     {
            //         while (reader.Read()) {
            //             // Read first resultset
            //             Console.WriteLine(reader.GetString(0));
            //         }
            //         reader.NextResult();
            //         // while (reader.Read()) {
            //         //     // Read second resultset
            //         // }
            //     }
            // }

        }

        [EnableCors]
        [HttpPost]
        // public string[] Get()
        public void Post()
        {
        }
    }
}
