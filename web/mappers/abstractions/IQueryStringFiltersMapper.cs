using domain.dtos;
using web.models;

namespace web.mappers.abstractions
{
    public interface IQueryStringFiltersMapper
    {
        OrderFiltersDto Map(QueryStringFilters filters);
    }
}
