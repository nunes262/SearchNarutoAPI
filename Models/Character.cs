using System.Text.Json;
using System.Text.Json.Serialization;

namespace SearchNarutoAPI.Models;

public class Character
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public List<string>? Images { get; set; }

    [JsonPropertyName("jutsu")]
    public List<string>? Jutsu { get; set; }

    // Os campos "personal" e "rank" variam de formato na API
    // (objeto, lista ou nulo), por isso são lidos como JsonElement.
    [JsonPropertyName("personal")]
    public JsonElement Personal { get; set; }

    [JsonPropertyName("rank")]
    public JsonElement Rank { get; set; }

    [JsonIgnore]
    public string? Image => Images?.FirstOrDefault();

    // personal.clan (pode ser texto ou lista)
    [JsonIgnore]
    public List<string> Clan => ReadValues(Personal, "clan");

    // personal.affiliation (pode ser texto ou lista)
    [JsonIgnore]
    public List<string> Affiliation => ReadValues(Personal, "affiliation");

    // rank.ninjaRank (objeto no formato { "Part I": "Genin", ... })
    [JsonIgnore]
    public List<string> NinjaRank => ReadValues(Rank, "ninjaRank");

    private static List<string> ReadValues(JsonElement parent, string property)
    {
        var values = new List<string>();

        if (parent.ValueKind != JsonValueKind.Object ||
            !parent.TryGetProperty(property, out var element))
        {
            return values;
        }

        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                values.Add(element.GetString()!);
                break;

            case JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.String)
                    {
                        values.Add(item.GetString()!);
                    }
                }
                break;

            case JsonValueKind.Object:
                foreach (var item in element.EnumerateObject())
                {
                    if (item.Value.ValueKind == JsonValueKind.String)
                    {
                        values.Add($"{item.Value.GetString()} ({item.Name})");
                    }
                }
                break;
        }

        return values
            .Select(v => v.Trim())
            .Where(v => v.Length > 0)
            .ToList();
    }
}
