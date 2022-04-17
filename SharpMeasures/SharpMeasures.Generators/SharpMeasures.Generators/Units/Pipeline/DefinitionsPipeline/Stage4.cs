namespace SharpMeasures.Generators.Units.Pipeline.DefinitionsPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Units.Caching;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition,
        NamedType Quantity, bool Biased, IEnumerable<UnitAliasAttributeParameters> UnitAliases,
        IEnumerable<CachedDerivedUnitAttributeParameters> DerivedUnits,
        IEnumerable<FixedUnitAttributeParameters> FixedUnits, IEnumerable<OffsetUnitAttributeParameters> OffsetUnits,
        IEnumerable<PrefixedUnitAttributeParameters> PrefixedUnits, IEnumerable<ScaledUnitAttributeParameters> ScaledUnits);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<Stage3.Result> provider)
        => provider.Select(ExtractDerivations);

    private static Result ExtractDerivations(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<UnitAliasAttributeParameters> unitAliases = UnitAliasAttributeParameters.Parse(input.TypeSymbol);
        IEnumerable<CachedDerivedUnitAttributeParameters> derivedUnits
            = CachedDerivedUnitAttributeParameters.From(DerivedUnitAttributeParameters.Parse(input.TypeSymbol));
        IEnumerable<FixedUnitAttributeParameters> fixedUnits = FixedUnitAttributeParameters.Parse(input.TypeSymbol);
        IEnumerable<OffsetUnitAttributeParameters> offsetUnits = OffsetUnitAttributeParameters.Parse(input.TypeSymbol);
        IEnumerable<PrefixedUnitAttributeParameters> prefixedUnits = PrefixedUnitAttributeParameters.Parse(input.TypeSymbol);
        IEnumerable<ScaledUnitAttributeParameters> scaledUnits = ScaledUnitAttributeParameters.Parse(input.TypeSymbol);

        return new Result(input.Documentation, input.TypeDefinition, input.Quantity, input.Biased, unitAliases, derivedUnits,
            fixedUnits, offsetUnits, prefixedUnits, scaledUnits);
    }
}
