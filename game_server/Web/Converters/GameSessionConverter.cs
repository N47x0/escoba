using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class GameSessionConverter
	{
		public static GameSessionDto Convert(GameSession source)
		{
			if (source == null)
			{
				return null;
			}
			return new GameSessionDto
			{
				GameSessionId = source.GameSessionId,
				GameInfoId = source.GameInfoId,
				GameInfo = source.GameInfo,
				GameSessionState = source.GameSessionState,
				// UserGameSessions = source.UserGameSessions,
				// GameStates = source.GameStates
			};
		}
		public static IEnumerable<GameSessionDto> Convert(IEnumerable<GameSession> source)
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