using System.Text.Json.Serialization;

namespace Application.Features.Dogs.Queries.GetAll;

public class GetDogsResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    [JsonPropertyName("tail_length")]
    public int TailLength { get; set; }
    public int Weight { get; set; }
}
