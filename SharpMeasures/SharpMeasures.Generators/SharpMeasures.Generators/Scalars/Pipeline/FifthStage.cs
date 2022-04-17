namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Units.Caching;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class FifthStage
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition, string Unit, bool Biased,
        string UnitQuantity, bool PrimaryQuantityForUnit, ThirdStage.Settings Settings, IEnumerable<UnitAliasAttributeParameters> UnitAliases,
        IEnumerable<CachedDerivedUnitAttributeParameters> DerivedUnits, IEnumerable<FixedUnitAttributeParameters> FixedUnits,
        IEnumerable<OffsetUnitAttributeParameters> OffsetUnits, IEnumerable<PrefixedUnitAttributeParameters> PrefixedUnits,
        IEnumerable<ScaledUnitAttributeParameters> ScaledUnits, PowerData? InvertibleOperations, PowerData? SquarableOperations,
        PowerData? CubableOperations, PowerData? SquareRootableOperations, PowerData? CubeRootableOperations, IncludeUnitsAttributeParameters? IncludedUnits,
        ExcludeUnitsAttributeParameters? ExcludedUnits, IncludeBasesAttributeParameters? IncludedBases, ExcludeBasesAttributeParameters? ExcludedBases);

    public readonly record struct PowerData(string Quantity, IEnumerable<string> SecondaryQuantities)
    {
        public static PowerData? Parse(INamedTypeSymbol? quantity, IEnumerable<INamedTypeSymbol>? secondaryQuantities)
        {
            if (quantity is null)
            {
                return null;
            }

            return new PowerData(quantity.ToDisplayString(), toNames());

            IEnumerable<string> toNames()
            {
                if (secondaryQuantities is null)
                {
                    yield break;
                }

                foreach (INamedTypeSymbol secondaryQuantity in secondaryQuantities)
                {
                    yield return secondaryQuantity.ToDisplayString();
                }
            }
        }
    }

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<FourthStage.Result> provider)
        => provider.Select(ExtractAttributeInformationAndTypeData);

    private static Result ExtractAttributeInformationAndTypeData(FourthStage.Result input, CancellationToken _)
    {
        InvertibleQuantityAttributeParameters? invertible = InvertibleQuantityAttributeParameters.Parse(input.TypeSymbol);
        SquarableQuantityAttributeParameters? squarable = SquarableQuantityAttributeParameters.Parse(input.TypeSymbol);
        CubableQuantityAttributeParameters? cubable = CubableQuantityAttributeParameters.Parse(input.TypeSymbol);
        SquareRootableQuantityAttributeParameters? squareRootable = SquareRootableQuantityAttributeParameters.Parse(input.TypeSymbol);
        CubeRootableQuantityAttributeParameters? cubeRootable = CubeRootableQuantityAttributeParameters.Parse(input.TypeSymbol);

        PowerData? invertibleParsed = PowerData.Parse(invertible?.Quantity, invertible?.SecondaryQuantities);
        PowerData? squarableParsed = PowerData.Parse(squarable?.Quantity, squarable?.SecondaryQuantities);
        PowerData? cubableParsed = PowerData.Parse(cubable?.Quantity, cubable?.SecondaryQuantities);
        PowerData? squareRootableParsed = PowerData.Parse(squareRootable?.Quantity, squareRootable?.SecondaryQuantities);
        PowerData? cubeRootableParsed = PowerData.Parse(cubeRootable?.Quantity, cubeRootable?.SecondaryQuantities);

        IncludeUnitsAttributeParameters? includedUnits = IncludeUnitsAttributeParameters.Parse(input.TypeSymbol);
        ExcludeUnitsAttributeParameters? excludedUnits = ExcludeUnitsAttributeParameters.Parse(input.TypeSymbol);
        IncludeBasesAttributeParameters? includedBases = IncludeBasesAttributeParameters.Parse(input.TypeSymbol);
        ExcludeBasesAttributeParameters? excludedBases = ExcludeBasesAttributeParameters.Parse(input.TypeSymbol);

        return new Result(input.Documentation, DefinedType.FromSymbol(input.TypeSymbol), input.Unit, input.Biased, input.UnitQuantity, input.PrimaryQuantityForUnit,
            input.Settings, input.UnitAliases, input.DerivedUnits, input.FixedUnits, input.OffsetUnits, input.PrefixedUnits, input.ScaledUnits,
            invertibleParsed, squarableParsed, cubableParsed, squareRootableParsed, cubeRootableParsed, includedUnits, excludedUnits, includedBases,
            excludedBases);
    }
}
