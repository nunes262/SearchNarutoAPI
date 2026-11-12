using System.Text.Json.Serialization;

namespace SearchNarutoAPI.Models;

public class CharacterResponse
{
    [JsonPropertyName("characters")]
    public List<Character> Characters { get; set; } = new();

    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonIgnore]
    public int TotalPages => PageSize > 0
        ? (int)Math.Ceiling(Total / (double)PageSize)
        : 1;
}
