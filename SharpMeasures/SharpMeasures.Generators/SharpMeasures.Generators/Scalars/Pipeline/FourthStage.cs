namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Units.Caching;
using SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Threading;

internal static class FourthStage
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, INamedTypeSymbol TypeSymbol, string Unit, bool Biased,
        string UnitQuantity, bool PrimaryQuantityForUnit, ThirdStage.Settings Settings, IEnumerable<UnitAliasAttributeParameters> UnitAliases,
        IEnumerable<CachedDerivedUnitAttributeParameters> DerivedUnits, IEnumerable<FixedUnitAttributeParameters> FixedUnits,
        IEnumerable<OffsetUnitAttributeParameters> OffsetUnits, IEnumerable<PrefixedUnitAttributeParameters> PrefixedUnits,
        IEnumerable<ScaledUnitAttributeParameters> ScaledUnits);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => provider.Select(ExtractUnitData).WhereNotNull();

    private static Result? ExtractUnitData(ThirdStage.Result input, CancellationToken _)
    {
        if (GeneratedUnitAttributeParameters.Parser.ParseSingle(input.UnitSymbol).Quantity?.ToDisplayString() is not string unitQuantity)
        {
            return null;
        }    

        IEnumerable <UnitAliasAttributeParameters> aliasInstances = UnitAliasAttributeParameters.Parser.Parse(input.UnitSymbol);
        IEnumerable<CachedDerivedUnitAttributeParameters> derivedInstances
            = CachedDerivedUnitAttributeParameters.From(DerivedUnitAttributeParameters.Parser.Parse(input.UnitSymbol));
        IEnumerable<FixedUnitAttributeParameters> fixedInstances = FixedUnitAttributeParameters.Parser.Parse(input.UnitSymbol);
        IEnumerable<OffsetUnitAttributeParameters> offsetInstances = OffsetUnitAttributeParameters.Parser.Parse(input.UnitSymbol);
        IEnumerable<PrefixedUnitAttributeParameters> prefixedInstances = PrefixedUnitAttributeParameters.Parser.Parse(input.UnitSymbol);
        IEnumerable<ScaledUnitAttributeParameters> scaledInstances = ScaledUnitAttributeParameters.Parser.Parse(input.UnitSymbol);

        return new Result(input.Documentation, input.TypeSymbol, input.UnitSymbol.ToDisplayString(), input.Biased, unitQuantity, input.PrimaryQuantityForUnit,
            input.Settings, aliasInstances, derivedInstances, fixedInstances, offsetInstances, prefixedInstances, scaledInstances);
    }
}
