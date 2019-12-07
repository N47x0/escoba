using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class UserStatisticConverter
	{
		public static UserStatisticDto Convert(UserStatistic source)
		{
			if (source == null)
			{
				return null;
			}
			return new UserStatisticDto
			{
				UserStatisticId = source.UserStatisticId,
				UserId = source.UserId,
				User = source.User,
				GameInfoId = source.GameInfoId,
				GameInfo = source.GameInfo,
				NumberOfPlays = source.NumberOfPlays,
				Wins = source.Wins,
				Losses = source.Losses,
				Draws = source.Draws
			};
		}
		public static IEnumerable<UserStatisticDto> Convert(IEnumerable<UserStatistic> source)
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