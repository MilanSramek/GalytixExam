using System.ComponentModel.DataAnnotations;

namespace Persistence;

public sealed class CsvPremiumsRepositorySettings
{
    [Required]
    public required string Path { get; set; }
}
