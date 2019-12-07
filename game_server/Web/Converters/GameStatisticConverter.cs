using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class GameStatisticConverter
	{
		public static GameStatisticDto Convert(GameStatistic source)
		{
			if (source == null)
			{
				return null;
			}
			return new GameStatisticDto
			{
				GameStatisticId = source.GameStatisticId,
				GameInfoId = source.GameInfoId,
				TimesPlayed = source.TimesPlayed,
				HumanWins = source.HumanWins,
				AiWins = source.AiWins,
				HumanLosses = source.HumanLosses,
				AiLosses = source.AiLosses,
				HumanDraws = source.HumanDraws,
				AiDraws = source.AiDraws
			};
		}
		public static IEnumerable<GameStatisticDto> Convert(IEnumerable<GameStatistic> source)
		{
			if (source == null)
			{
				return null;
			}
			return source
				.Select(x => Convert(x));
		}
	}
}