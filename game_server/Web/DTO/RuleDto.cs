using Newtonsoft.Json;
using System;

namespace game_server.Web.DTO
{
    public class RuleDtoInput
    {
        [JsonProperty("gameName")]
        public string GameName { get; set; }

        [JsonProperty("ruleName")]
        public string RuleName { get; set; }
        [JsonProperty("ruleText")]
        public string RuleText { get; set; }
    }
    public class RuleDtoOutput
    {
        [JsonProperty("ruleId")]
        public Guid RuleId { get; set; }

        [JsonProperty("gameInfoId")]
        public Guid GameInfoId { get; set; }

        [JsonProperty("ruleName")]
        public string RuleName { get; set; }
        [JsonProperty("ruleText")]
        public string RuleText { get; set; }
    }
}