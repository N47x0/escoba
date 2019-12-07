using game_server.Context;

namespace game_server.Factory
{
  public interface IGameSessionModelDbContextFactory
  {
    GameSessionModelDbContext CreateDbContext();
  }
}