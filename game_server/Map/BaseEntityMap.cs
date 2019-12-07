using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace game_server.Map
{
    public abstract class BaseEntityMapy<TEntityType> : IEntityTypeMap
        where TEntityType : class
        {
            public void Map(ModelBuilder builder)
            { 
                InternalMap(builder.Entity<TEntityType>());
            }

            protected abstract void InternalMap(EntityTypeBuilder<TEntityType> builder);
        }
}