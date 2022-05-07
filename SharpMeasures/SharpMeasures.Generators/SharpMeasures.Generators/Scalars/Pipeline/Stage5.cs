namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class Stage5
{
    public readonly record struct Result(DefinedType TypeDefinition, string Unit, bool Biased, string UnitQuantity, DocumentationFile Documentation,
        IEnumerable<UnitAliasParameters> UnitAliases, IEnumerable<DerivedUnitParameters> DerivedUnits,
        IEnumerable<FixedUnitParameters> FixedUnits, IEnumerable<OffsetUnitParameters> OffsetUnits,
        IEnumerable<PrefixedUnitParameters> PrefixedUnits, IEnumerable<ScaledUnitParameters> ScaledUnits,
        PowerData? InvertibleOperations, PowerData? SquarableOperations, PowerData? CubableOperations, PowerData? SquareRootableOperations,
        PowerData? CubeRootableOperations, IncludeUnitsParameters IncludedUnits, ExcludeUnitsParameters ExcludedUnits,
        IncludeBasesParameters IncludedBases, ExcludeBasesParameters ExcludedBases);

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

    public static IncrementalValuesProvider<Result> Attach(IncrementalValuesProvider<Stage4.Result> inputProvider)
        => inputProvider.Select(ExtractAttributeInformationAndTypeData);

    private static Result ExtractAttributeInformationAndTypeData(Stage4.Result input, CancellationToken _)
    {
        InvertibleQuantityParameters? invertible = InvertibleQuantityParser.Parser.ParseFirst(input.TypeSymbol);
        SquarableQuantityParameters? squarable = SquarableQuantityParser.Parser.ParseFirst(input.TypeSymbol);
        CubableQuantityParameters? cubable = CubableQuantityParser.Parser.ParseFirst(input.TypeSymbol);
        SquareRootableQuantityParameters? squareRootable = SquareRootableQuantityParser.Parser.ParseFirst(input.TypeSymbol);
        CubeRootableQuantityParameters? cubeRootable = CubeRootableQuantityParser.Parser.ParseFirst(input.TypeSymbol);

        PowerData? invertibleParsed = PowerData.Parse(invertible?.Quantity, invertible?.SecondaryQuantities);
        PowerData? squarableParsed = PowerData.Parse(squarable?.Quantity, squarable?.SecondaryQuantities);
        PowerData? cubableParsed = PowerData.Parse(cubable?.Quantity, cubable?.SecondaryQuantities);
        PowerData? squareRootableParsed = PowerData.Parse(squareRootable?.Quantity, squareRootable?.SecondaryQuantities);
        PowerData? cubeRootableParsed = PowerData.Parse(cubeRootable?.Quantity, cubeRootable?.SecondaryQuantities);

        IncludeUnitsParameters includedUnits = IncludeUnitsParser.Parser.ParseFirst(input.TypeSymbol) ?? IncludeUnitsParameters.Empty;
        ExcludeUnitsParameters excludedUnits = ExcludeUnitsParser.Parser.ParseFirst(input.TypeSymbol) ?? ExcludeUnitsParameters.Empty;
        IncludeBasesParameters includedBases = IncludeBasesParser.Parser.ParseFirst(input.TypeSymbol) ?? IncludeBasesParameters.Empty;
        ExcludeBasesParameters excludedBases = ExcludeBasesParser.Parser.ParseFirst(input.TypeSymbol) ?? ExcludeBasesParameters.Empty;

        return new Result(DefinedType.FromSymbol(input.TypeSymbol), input.Unit, input.Biased, input.UnitQuantity, input.Documentation,
            input.UnitAliases, input.DerivedUnits, input.FixedUnits, input.OffsetUnits, input.PrefixedUnits, input.ScaledUnits,
            invertibleParsed, squarableParsed, cubableParsed, squareRootableParsed, cubeRootableParsed, includedUnits, excludedUnits, includedBases,
            excludedBases);
    }
}
