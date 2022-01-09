namespace AirQuality.Web.Models.OpenAq
{
    public record HttpValidationError
    {
        public IList<ValidationError> Detail { get; set; } = new List<ValidationError>();
    }
}
