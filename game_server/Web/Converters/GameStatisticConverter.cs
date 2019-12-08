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
				GameSessionId = source.GameSessionId,
				UserId = source.UserId,
				User = source.User,
				UserStatisticId = source.UserStatisticId,
				UserStatistic = source.UserStatistic,
				UserGameSessions = source.UserGameSessions,
				FinalScore = source.FinalScore,
				HumanWin = source.HumanWin,
				AiWin = source.AiWin,
				Draw = source.Draw,
				GameComplete = source.GameComplete,
				GameStart = source.GameStart,
				GameEnd = source.GameEnd
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