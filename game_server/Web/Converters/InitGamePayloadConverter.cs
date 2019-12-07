using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class InitGamePayloadConverter
	{
		public static InitGamePayloadDto Convert(InitGamePayload source)
		{
			if (source == null)
			{
				return null;
			}
			return new InitGamePayloadDto
			{
				GameSessionId = source.GameSessionId,
				GameState = source.GameState
			};
		}
		public static IEnumerable<InitGamePayloadDto> Convert(IEnumerable<InitGamePayload> source)
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