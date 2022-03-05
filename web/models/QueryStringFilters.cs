using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace web.models
{
    public class QueryStringFilters
    {
        public int? id { get; set; }
        public decimal? totalPrice { get; set; }
    }
}
