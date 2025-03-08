using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HarryPotter.Classes.CustomHats;

public class SkinsConfigFile
{
    [JsonPropertyName("hats")] public List<CustomHat> Hats { get; set; }
}