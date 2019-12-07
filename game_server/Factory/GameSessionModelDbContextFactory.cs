using game_server.Context;

namespace game_server.Factory
{
  public class GameSessionModelDbContextFactory : IGameSessionModelDbContextFactory
  {
    private readonly GameSessionModelDbContextOptions options;
    public GameSessionModelDbContextFactory(GameSessionModelDbContextOptions options)
    {
      this.options = options;
    }
    public GameSessionModelDbContext CreateDbContext()
    {
      return new GameSessionModelDbContext(options);
    }
  }
}