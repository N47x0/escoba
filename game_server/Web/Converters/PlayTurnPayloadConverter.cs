using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class PlayTurnPayloadConverter
	{
		public static PlayTurnPayloadDto Convert(PlayTurnPayload source)
		{
			if (source == null)
			{
				return null;
			}
			return new PlayTurnPayloadDto
			{
				GameSessionId = source.GameSessionId,
				GameState = source.GameState
			};
		}
		public static IEnumerable<PlayTurnPayloadDto> Convert(IEnumerable<PlayTurnPayload> source)
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