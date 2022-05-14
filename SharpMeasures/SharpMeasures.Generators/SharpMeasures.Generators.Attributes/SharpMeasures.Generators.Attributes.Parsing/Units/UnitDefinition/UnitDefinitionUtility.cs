namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public static class UnitDefinitionUtility
{
    public static IEnumerable<string> GetNamesOfAllUnits(INamedTypeSymbol typeSymbol)
    {
        return getNames(DerivedUnitParser.Parser)
            .Concat(getNames(UnitAliasParser.Parser))
            .Concat(getNames(FixedUnitParser.Parser))
            .Concat(getNames(OffsetUnitParser.Parser))
            .Concat(getNames(PrefixedUnitParser.Parser))
            .Concat(getNames(ScaledUnitParser.Parser));

        IEnumerable<string> getNames<TParameters>(AArgumentParser<TParameters> parser)
            where TParameters : IUnitDefinition
        {
            foreach (IUnitDefinition unit in parser.Parse(typeSymbol))
            {
                yield return unit.Name;
            }
        }
    }
}
