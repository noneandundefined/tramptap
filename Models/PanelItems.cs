using System.Text.Json.Serialization;

namespace tramptap.Models
{
    public class PanelItems
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("path_view")]
        public string PathView { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
