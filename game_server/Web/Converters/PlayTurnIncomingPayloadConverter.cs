using System.Collections.Generic;
using System.Linq;
using game_server.Database.Models;
using game_server.Web.DTO;

namespace game_server.Web.Converters
{
	public static class PlayTurnIncomingPayloadConverter
	{
		public static PlayTurnIncomingPayloadDto Convert(PlayTurnIncomingPayload source)
		{
			if (source == null)
			{
				return null;
			}
			return new PlayTurnIncomingPayloadDto
			{
				GameSessionId = source.GameSessionId,
				CardsPlayed = source.CardsPlayed
			};
		}
		public static IEnumerable<PlayTurnIncomingPayloadDto> Convert(IEnumerable<PlayTurnIncomingPayload> source)
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