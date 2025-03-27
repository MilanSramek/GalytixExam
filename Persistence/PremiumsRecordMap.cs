using CsvHelper.Configuration;

namespace Persistence;

internal sealed class PremiumsRecordMap : ClassMap<PremiumsRecord>
{
    public PremiumsRecordMap()
    {
        Map(_ => _.Country).Name("country");
        Map(_ => _.VariableId).Name("variableId");
        Map(_ => _.VariableName).Name("variableName");
        Map(_ => _.LineOfBusiness).Name("lineOfBusiness");
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
