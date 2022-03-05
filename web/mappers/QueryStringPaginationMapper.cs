using domain.dtos;
using web.mappers.abstractions;
using web.models;

namespace web.mappers
{
    public class QueryStringPaginationMapper : IQueryStringPaginationMapper
    {
        private int _defaultLimit => 50;
        private int _defaultOffset => 0;

        public PaginationDto Map(QueryStringPagination pagination)
        {
            return new PaginationDto()
            {
                Limit = pagination.limit ?? _defaultLimit,
                Offset = pagination.offset ?? _defaultOffset,
                OrderBy = pagination.orderBy ?? string.Empty
            };
        }
    }
}
