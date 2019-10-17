using GameManager;

namespace broom.Models
{
    public class SessionViewModel
    {
        public long ServiceId { get; set; }
        public string Name { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Game _Game { get; set; }

    }
}