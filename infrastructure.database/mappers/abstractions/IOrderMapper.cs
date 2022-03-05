using domain.entities;
using infrastructure.database.model;

namespace infrastructure.database.mappers.abstractions
{
    public interface IOrderMapper
    {
        Order Map(OrderDb orderDb);
    }
}
