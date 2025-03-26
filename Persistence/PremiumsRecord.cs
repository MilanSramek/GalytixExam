namespace Persistence;

internal sealed class PremiumsRecord
{
    public required string Country { get; set; }
    public required string VariableId { get; set; }
    public required string VariableName { get; set; }
    public required string LineOfBusiness { get; set; }
    public required Dictionary<string, string> ExtraColumns { get; set; } = [];
}
