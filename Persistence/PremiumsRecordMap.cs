using CsvHelper.Configuration;

namespace Persistence;

internal sealed class PremiumsRecordMap : ClassMap<PremiumsRecord>
{
    public PremiumsRecordMap()
    {
        Map(_ => _.Country);
        Map(_ => _.VariableId);
        Map(_ => _.VariableName);
        Map(_ => _.LineOfBusiness);
        Map(_ => _.ExtraColumns).Convert(args =>
        {
            var extraColumns = new Dictionary<string, string>();
            var row = args.Row;

            foreach (var header in row.HeaderRecord ?? [])
            {
                if (header.StartsWith('Y'))
                {
                    extraColumns[header] = row.GetField(header)!;
                }
            }

            return extraColumns;
        });
    }
}
