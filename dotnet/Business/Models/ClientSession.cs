using System;
using GameManager;

namespace Business.Models {
        
  public class ClientSession {
    private static int m_Counter = 0;
    public long ServiceId { get; set; }
    public int Id { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public Game _Game { get; set; }
    public int NewId() 
    {
    // return this.Id = System.Threading.Interlocked.Increment(ref m_Counter);
    return this.Id = System.Threading.Interlocked.Increment(ref m_Counter);
    }
  }
}
