using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
    public class GameStatisticDto
    {
        [JsonProperty("gameStatisticId")]
        public Guid GameStatisticId { get; set; }

        [JsonProperty("gameInfoId")]
        public Guid GameInfoId { get; set; }

        [JsonProperty("gameSessionId")]
        public Guid GameSessionId { get; set; }

        [JsonProperty("userGameSessions")]
        public ICollection<UserGameSession> UserGameSessions { get; set; }

        [JsonProperty("finalScore")]
        public string FinalScore { get; set; }

        [JsonProperty("humanWin")]
        public bool HumanWin { get; set; }

        [JsonProperty("aiWis")]
        public bool AiWin { get; set; }

        [JsonProperty("draw")]
        public bool Draw { get; set; }

        [JsonProperty("gameStart")]
        public DateTime GameStart { get; set; }

        [JsonProperty("gameEnd")]
        public DateTime GameEnd { get; set; }


    }
}