using System;
using Newtonsoft.Json;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
	public class UserGameSessionDto
	{

        [JsonProperty("userGameSessionId")]
        public Guid UserGameSessionId { get; set; }

        
        [JsonProperty("gameSessionId")]
        public Guid GameSessionId { get; set; }


        [JsonProperty("gameInfoId")]
        public Guid GameInfoId { get; set; }

        [JsonProperty("gameInfo")]
        public GameInfo GameInfo { get; set; }
        

        [JsonProperty("gameStatisticId")]
        public Guid GameStatisticId { get; set; }


        [JsonProperty("gameSession")]
        public GameSession GameSession { get; set; }


        [JsonProperty("gameStatistic")]
        public GameStatistic GameStatistic { get; set; }


        [JsonProperty("userId")]
        public Guid UserId { get; set; }


        [JsonProperty("user")]
        public User User { get; set; }

	}
}