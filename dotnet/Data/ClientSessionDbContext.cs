using Microsoft.EntityFrameworkCore;
using GameManager;


public class ClientSessionDbContext : DbContext {
    
    public ClientSessionDbContext(DbContextOptions<ClientSessionDbContext> options) 
        : base(options) 
        {

        }

        // public DbSet<ClientSession> ClientSessions { get; set; }
}