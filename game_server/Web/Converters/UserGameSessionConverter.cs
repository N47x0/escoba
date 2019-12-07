using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class UserGameSessionConverter
	{
		public static UserGameSessionDto Convert(UserGameSession source)
		{
			if (source == null)
			{
				return null;
			}
			return new UserGameSessionDto
			{
				UserGameSessionId = source.UserGameSessionId,
				GameSessionId = source.GameSessionId,
				GameStatisticId = source.GameStatisticId,
				GameInfoId = source.GameInfoId,
				GameInfo = source.GameInfo,
				GameSession = source.GameSession,
				GameStatistic = source.GameStatistic,
				UserId = source.UserId,
				User = source.User
			};
		}
		public static IEnumerable<UserGameSessionDto> Convert(IEnumerable<UserGameSession> source)
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