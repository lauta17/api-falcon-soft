namespace web.models
{
    public class QueryStringPagination
    {
        private const int _maxItemsPerPage = 50;

        public int? limit { get; set; }
        public int? offset { get; set; }
        public string? orderBy { get; set; }

        public QueryStringPagination() 
        {
            limit = _maxItemsPerPage;
            offset = 0;
            orderBy = "Id asc";
        }
    }
}
