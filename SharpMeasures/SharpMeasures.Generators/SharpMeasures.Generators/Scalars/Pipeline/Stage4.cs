namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(DocumentationFile Documentation, INamedTypeSymbol TypeSymbol, string Unit, bool Biased,
        string UnitQuantity, IEnumerable<UnitAliasParameters> UnitAliases,
        IEnumerable<DerivedUnitParameters> DerivedUnits, IEnumerable<FixedUnitParameters> FixedUnits,
        IEnumerable<OffsetUnitParameters> OffsetUnits, IEnumerable<PrefixedUnitParameters> PrefixedUnits,
        IEnumerable<ScaledUnitParameters> ScaledUnits);

    public static IncrementalValuesProvider<Result> Attach(IncrementalValuesProvider<Stage3.Result> inputProvider)
        => inputProvider.Select(ExtractUnitData).WhereNotNull();

    private static Result? ExtractUnitData(Stage3.Result input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirst(input.UnitSymbol)?.Quantity?.ToDisplayString() is not string unitQuantity)
        {
            return null;
        }    

        IEnumerable<UnitAliasParameters> aliasInstances = UnitAliasParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<DerivedUnitParameters> derivedInstances = DerivedUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<FixedUnitParameters> fixedInstances = FixedUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<OffsetUnitParameters> offsetInstances = OffsetUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<PrefixedUnitParameters> prefixedInstances = PrefixedUnitParser.Parser.Parse(input.UnitSymbol);
        IEnumerable<ScaledUnitParameters> scaledInstances = ScaledUnitParser.Parser.Parse(input.UnitSymbol);

        return new Result(input.Documentation, input.TypeSymbol, input.UnitSymbol.ToDisplayString(), input.Biased, unitQuantity,
            aliasInstances, derivedInstances, fixedInstances, offsetInstances, prefixedInstances, scaledInstances);
    }
}
