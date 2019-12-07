using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using game_server.Context;
using game_server.Database.Models;
using game_server.Web.Converters;
using game_server.Factory;

namespace game_server.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IGameSessionModelDbContextFactory dbContextFactory;

        public UserController(IGameSessionModelDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet("user/{action}/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id, CancellationToken cancellationToken)
        {
            Guid _userId = Guid.Parse(id);
            using (var context = dbContextFactory.CreateDbContext())
            {
                var user = await context.DbSet<User>()
                    .FirstOrDefaultAsync(x => x.UserId == _userId, cancellationToken);

                if (user == null)
                {
                    return NotFound();
                }

                var dtoUser = UserConverter.Convert(user);

                return Ok(dtoUser);
            }
        }

        [HttpGet("users/{action}")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var users = await context.DbSet<User>()
                    .ToListAsync(cancellationToken);

                var dtoUsers = UserConverter.Convert(users);

                return Ok(dtoUsers);
            }
        }
    }
}