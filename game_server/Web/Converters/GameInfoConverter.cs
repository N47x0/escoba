using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class GameInfoConverter
	{
		public static GameInfoDto Convert(GameInfo source)
		{
			if (source == null)
			{
				return null;
			}
			return new GameInfoDto
			{
				GameInfoId = source.GameInfoId,
				GameName = source.GameName,
				Rules = source.Rules,
				GameSessions = source.GameSessions,
				GameStatistics = source.GameStatistics
			};
		}
		public static IEnumerable<GameInfoDto> Convert(IEnumerable<GameInfo> source)
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