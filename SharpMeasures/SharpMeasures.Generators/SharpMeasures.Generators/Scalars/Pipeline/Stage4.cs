namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(INamedTypeSymbol TypeSymbol, string Unit, bool Biased, string UnitQuantity, DocumentationFile Documentation,
        IEnumerable<UnitAliasDefinition> UnitAliases, IEnumerable<DerivedUnitDefinition> DerivedUnits, IEnumerable<FixedUnitDefinition> FixedUnits,
        IEnumerable<OffsetUnitDefinition> OffsetUnits, IEnumerable<PrefixedUnitDefinition> PrefixedUnits, IEnumerable<ScaledUnitDefinition> ScaledUnits);

    public static IncrementalValuesProvider<Result> Attach(IncrementalValuesProvider<Stage3.Result> inputProvider)
        => inputProvider.Select(ExtractUnitData).WhereNotNull();

    private static Result? ExtractUnitData(Stage3.Result input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirst(input.UnitSymbol)?.Quantity?.ToDisplayString() is not string unitQuantity)
        {
            return null;
        }    

        IEnumerable<UnitAliasDefinition> aliasInstances = UnitAliasParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<DerivedUnitDefinition> derivedInstances = DerivedUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<FixedUnitDefinition> fixedInstances = FixedUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<OffsetUnitDefinition> offsetInstances = OffsetUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<PrefixedUnitDefinition> prefixedInstances = PrefixedUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<ScaledUnitDefinition> scaledInstances = ScaledUnitParser.Parser.Parse(input.UnitSymbol);

        return new Result(input.TypeSymbol, input.UnitSymbol.ToDisplayString(), input.Biased, unitQuantity, input.Documentation,
            aliasInstances, derivedInstances, fixedInstances, offsetInstances, prefixedInstances, scaledInstances);
    }
}
