using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Models
{
    public class HistoryItem
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public HistoryItem() { }
        public HistoryItem(LocationsRow loc) 
        {
            Id = loc.Id;
            Location = loc.Name;
            City = loc.City;
            Country = loc.Country;
            Date = DateTime.Now;
        }
    }
}
