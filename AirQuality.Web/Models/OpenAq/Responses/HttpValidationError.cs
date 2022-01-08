namespace AirQuality.Web.Models.OpenAq.Responses
{
    public record HttpValidationError
    {
        public IList<ValidationError> Detail { get; set; } = new List<ValidationError>();
    }
}
