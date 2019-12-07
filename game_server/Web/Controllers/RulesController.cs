using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using game_server.Context;
using game_server.Database.Models;
using game_server.Web.Converters;
using game_server.Web.DTO;
using game_server.Factory;

namespace game_server.Web.Controllers
{
  public class RulesController : Controller
  {
    private readonly IGameSessionModelDbContextFactory dbContextFactory;

    public RulesController(IGameSessionModelDbContextFactory dbContextFactory)
      {
          this.dbContextFactory = dbContextFactory;
      }

    [HttpGet("rule/{action}/{id}")]
    public async Task<IActionResult> GetRule([FromRoute] string id, CancellationToken cancellationToken)
    {
      Guid _ruleId = Guid.Parse(id);
      using (var context = dbContextFactory.CreateDbContext())
      {
        // context.Set<Rule>().FirstOrDefaultAsync();
        var rule = await context.DbSet<Rule>()
          .FirstOrDefaultAsync(x => x.RuleId == _ruleId, cancellationToken);

        if (rule == null)
        {
          return NotFound();
        }

        var dtoRule = RuleConverter.ConvertOutput(rule);

        return Ok(dtoRule);
      }
    }

    [HttpGet("rules/{action}")]
    public async Task<IActionResult> GetAllRules(CancellationToken cancellationToken)
    {
      using (var context = dbContextFactory.CreateDbContext())
      {
        var rules = await context.DbSet<Rule>()
          .ToListAsync(cancellationToken);

        var dtoRules = RuleConverter.ConvertOutput(rules);

        return Ok(dtoRules);
      }
    }

    [HttpPost("rules/{action}")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRule(
        // [Bind("gameName, ruleName, ruleText")] RuleDtoInput ruleInput, CancellationToken cancellationToken)
        [FromBody] RuleDtoInput ruleInput, CancellationToken cancellationToken)
    {
      using (var context = dbContextFactory.CreateDbContext())
      {
        // if(ModelState.IsValid)

        if (ruleInput == null)
        {
          return NotFound();
        }

        // map rule to game

        GameInfo gameInfo = new GameInfo();
        gameInfo = await context.DbSet<GameInfo>()
            .FirstOrDefaultAsync(x => x.GameName == ruleInput.GameName, cancellationToken);

        Rule newRule = new Rule {
          RuleId = Guid.NewGuid(),
          GameInfoId = gameInfo.GameInfoId,
          RuleName = ruleInput.RuleName,
          RuleText = ruleInput.RuleText
        };

        context.Add(newRule);
        
        await context.SaveChangesAsync();

        var dtoRuleOutput = RuleConverter.ConvertOutput(newRule);

        return Ok(dtoRuleOutput);

      }
    }
  }
}