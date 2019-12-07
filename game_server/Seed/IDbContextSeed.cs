using Microsoft.EntityFrameworkCore;

namespace game_server.Seed
{
    public interface IDbContextSeed
    {
        void Seed(ModelBuilder modelBuilder);
    }
}