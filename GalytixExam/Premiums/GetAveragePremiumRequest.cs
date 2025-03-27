using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GalytixExam.Premiums;

public sealed class GetAveragePremiumRequest
{
    [Required]
    public required string Country { get; set; }

    [JsonPropertyName("lob")]
    public string[]? LineOfBusinessItems { get; set; }
};
