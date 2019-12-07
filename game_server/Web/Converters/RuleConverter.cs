using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using game_server.Database.Models;
using game_server.Web.DTO;
using game_server.Factory;
using game_server.Context;

namespace game_server.Web.Converters
{
	public class RuleConverter
	
	{
		public RuleDtoInput ConvertInput(RuleDtoInput source)
		{
			if (source == null)
			{
				return null;
			}
			return new RuleDtoInput
			{
			  GameName = source.GameName,
				RuleName = source.RuleName,
				RuleText = source.RuleText
			};
		}
		public IEnumerable<RuleDtoInput> ConvertInput(IEnumerable<RuleDtoInput> source)
		{
			if (source == null)
			{
				return null;
			}
			Func<RuleDtoInput, RuleDtoInput> convertInputGroup = (x) => 
			{
				return ConvertInput(x);
			};
			return source.Select(x => convertInputGroup(x));
		}
		public static RuleDtoOutput ConvertOutput(Rule source)
		{
			if (source == null)
			{
				return null;
			}
			return new RuleDtoOutput
			{
				RuleId = source.RuleId,
				GameInfoId = source.GameInfoId,
				RuleName = source.RuleName,
				RuleText = source.RuleText
			};
		}
		public static IEnumerable<RuleDtoOutput> ConvertOutput(IEnumerable<Rule> source)
		{
			if (source == null)
			{
				return null;
			}
			return source
				.Select(x => ConvertOutput(x));
		}
	}
}