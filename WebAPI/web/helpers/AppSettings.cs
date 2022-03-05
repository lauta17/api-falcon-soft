namespace web.helpers 
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public AppSettings() 
        {
            Secret = string.Empty;
        }
    }
}