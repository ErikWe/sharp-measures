namespace SharpMeasures.Generators.Units.Pipeline.DefinitionsPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Units.Caching;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System;
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
        => provider.Select(ExtractDefinitions);

    private static Result ExtractDefinitions(Stage3.Result input, CancellationToken token)
        => new(input.Documentation, input.TypeDefinition, input.Quantity, input.Biased, ExtractValidAliasDefinitions(input, token),
            ExtractValidDerivedDefinitions(input, token), ExtractValidFixedDefinitions(input, token), ExtractValidOffsetDefinitions(input, token),
            ExtractValidPrefixedDefinitions(input, token), ExtractValidScaledDefinitions(input, token));

    private static IEnumerable<UnitAliasAttributeParameters> ExtractValidAliasDefinitions(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<UnitAliasAttributeParameters> unitAliases = UnitAliasAttributeParameters.Parser.Parse(input.TypeSymbol);
        
        foreach (UnitAliasAttributeParameters unitAlias in unitAliases)
        {
            if (unitAlias.Name.Length > 0 && unitAlias.Plural.Length > 0 && unitAlias.AliasOf.Length > 0)
            {
                yield return unitAlias;
            }
        }
    }

    private static IEnumerable<CachedDerivedUnitAttributeParameters> ExtractValidDerivedDefinitions(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<CachedDerivedUnitAttributeParameters> derivedUnits
            = CachedDerivedUnitAttributeParameters.From(DerivedUnitAttributeParameters.Parser.Parse(input.TypeSymbol));

        foreach (CachedDerivedUnitAttributeParameters derivedUnit in derivedUnits)
        {
            if (derivedUnit.Name.Length > 0 && derivedUnit.Plural.Length > 0 && derivedUnit.Signature is not null && validUnits(derivedUnit))
            {
                yield return derivedUnit;
            }
        }

        static bool validUnits(CachedDerivedUnitAttributeParameters derivedUnit)
        {
            foreach (string unit in derivedUnit.Units)
            {
                if (string.IsNullOrEmpty(unit))
                {
                    return false;
                }
            }

            return true;
        }
    }

    private static IEnumerable<FixedUnitAttributeParameters> ExtractValidFixedDefinitions(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<FixedUnitAttributeParameters> fixedUnits = FixedUnitAttributeParameters.Parser.Parse(input.TypeSymbol);

        foreach (FixedUnitAttributeParameters fixedUnit in fixedUnits)
        {
            if (fixedUnit.Name.Length > 0 && fixedUnit.Plural.Length > 0)
            {
                yield return fixedUnit;
            }
        }
    }

    private static IEnumerable<OffsetUnitAttributeParameters> ExtractValidOffsetDefinitions(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<OffsetUnitAttributeParameters> offsetUnits = OffsetUnitAttributeParameters.Parser.Parse(input.TypeSymbol);

        foreach (OffsetUnitAttributeParameters offsetUnit in offsetUnits)
        {
            if (offsetUnit.Name.Length > 0 && offsetUnit.Plural.Length > 0 && offsetUnit.From.Length > 0)
            {
                yield return offsetUnit;
            }
        }
    }

    private static IEnumerable<PrefixedUnitAttributeParameters> ExtractValidPrefixedDefinitions(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<PrefixedUnitAttributeParameters> prefixedUnits = PrefixedUnitAttributeParameters.Parser.Parse(input.TypeSymbol);

        foreach (PrefixedUnitAttributeParameters prefixedUnit in prefixedUnits)
        {
            if (prefixedUnit.Name.Length > 0 && prefixedUnit.Plural.Length > 0 && prefixedUnit.From.Length > 0 && validPrefix(prefixedUnit))
            {
                yield return prefixedUnit;
            }
        }

        static bool validPrefix(PrefixedUnitAttributeParameters prefixedUnit)
        {
            return prefixedUnit.SpecifiedPrefixType switch
            {
                PrefixedUnitAttributeParameters.PrefixType.Metric => validMetricPrefix(prefixedUnit),
                PrefixedUnitAttributeParameters.PrefixType.Binary => validBinaryPrefix(prefixedUnit),
                _ => false
            };
        }

        static bool validMetricPrefix(PrefixedUnitAttributeParameters prefixedUnit)
            => Enum.IsDefined(typeof(MetricPrefixName), prefixedUnit.MetricPrefixName);

        static bool validBinaryPrefix(PrefixedUnitAttributeParameters prefixedUnit)
            => Enum.IsDefined(typeof(BinaryPrefixName), prefixedUnit.BinaryPrefixName);
    }

    private static IEnumerable<ScaledUnitAttributeParameters> ExtractValidScaledDefinitions(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<ScaledUnitAttributeParameters> scaledUnits = ScaledUnitAttributeParameters.Parser.Parse(input.TypeSymbol);

        foreach (ScaledUnitAttributeParameters scaledUnit in scaledUnits)
        {
            if (scaledUnit.Name.Length > 0 && scaledUnit.Plural.Length > 0 && scaledUnit.From.Length > 0)
            {
                yield return scaledUnit;
            }
        }
    }
}
