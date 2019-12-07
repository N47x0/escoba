using Microsoft.EntityFrameworkCore;

namespace game_server.Map
{
    public interface IEntityTypeMap
    {
        void Map(ModelBuilder builder);
    }
}