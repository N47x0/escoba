using Newtonsoft.Json;
using System;

namespace game_server.Web.DTO
{
    public class GameStatisticDto
    {
        [JsonProperty("gameStatisticId")]
        public Guid GameStatisticId { get; set; }
        [JsonProperty("gameInfoId")]
        public Guid GameInfoId { get; set; }
        [JsonProperty("timesPlayed")]
        public int TimesPlayed { get; set; }
        [JsonProperty("humanWins")]
        public int HumanWins { get; set; }
        [JsonProperty("aiWins")]
        public int AiWins { get; set; }
        [JsonProperty("humanLosses")]
        public int HumanLosses { get; set; }
        [JsonProperty("aiLosses")]
        public int AiLosses { get; set; }
        [JsonProperty("humanDraws")]
        public int HumanDraws { get; set; }
        [JsonProperty("aiDraws")]
        public int AiDraws { get; set; }
    }
}