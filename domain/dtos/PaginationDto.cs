namespace domain.dtos
{
    public class PaginationDto
    {
        private int _defaultLimit => 50;

        private int _defaultOffset => 0;

        public int Limit { get; set; }
        public int Offset { get; set; }
        public string OrderBy { get; set; }

        public PaginationDto() 
        {
            Limit = _defaultLimit;
            Offset = _defaultOffset;
            OrderBy = string.IsNullOrEmpty(OrderBy)
                ? "Id desc" 
                : string.Empty;
        }
    }
}
