using domain.dtos;
using web.mappers.abstractions;
using web.models;

namespace web.mappers
{
    public class QueryStringFiltersMapper : IQueryStringFiltersMapper
    {
        public OrderFiltersDto Map(QueryStringFilters filters)
        {
            return new OrderFiltersDto
            {
                Id = filters.id,
                TotalPrice = filters.totalPrice
            };
        }
    }
}
