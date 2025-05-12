namespace DbAppTest.model
{
    public class WeatherEntry
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public DateTime MomentInTime { get; set; }
        public decimal Temperature {  get; set; }
    }
}
