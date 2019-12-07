using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class UserConverter
	{
		public static UserDto Convert(User source)
		{
			if (source == null)
			{
				return null;
			}
			return new UserDto
			{
				UserId = source.UserId,
				FirstName = source.FirstName,
				LastName = source.LastName,
				EmailAddress = source.EmailAddress,
				UserGameSessions = source.UserGameSessions,
				UserStatistics = source.UserStatistics,
			};
		}
		public static IEnumerable<UserDto> Convert(IEnumerable<User> source)
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