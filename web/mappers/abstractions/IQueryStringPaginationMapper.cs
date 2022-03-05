using domain.dtos;
using web.models;

namespace web.mappers.abstractions
{
    public interface IQueryStringPaginationMapper
    {
        PaginationDto Map(QueryStringPagination pagination);
    }
}
