namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class BaseVectorParsingStage : AVectorParsingStage<SharpMeasuresVectorAttribute, RawSharpMeasuresVectorDefinition, SharpMeasuresVectorDefinition,
    RawParsedBaseVector, ParsedBaseVector>
{
    public IncrementalValuesProvider<BaseVector> InterfaceProvider { get; }

    public BaseVectorParsingStage(IncrementalGeneratorInitializationContext context) : base(context)
    {
        InterfaceProvider = ConstructInterfaces();
    }

    protected override IProcesser<IProcessingContext, RawSharpMeasuresVectorDefinition, SharpMeasuresVectorDefinition> Processer
        => new SharpMeasuresVectorProcesser(SharpMeasuresVectorDiagnostics.Instance);

    protected override RawSharpMeasuresVectorDefinition? Parse(INamedTypeSymbol typeSymbol) => SharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol);

    protected override RawParsedBaseVector ConstructParsed(DefinedType type, MinimalLocation location, SharpMeasuresVectorDefinition definition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawVectorConstantDefinition> vectorConstants, IEnumerable<RawConvertibleQuantityDefinition> dimensionalEquivalences)
    {
        return new(type, location, definition, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);
    }

    protected override ParsedBaseVector ConstructProcessed(DefinedType type, MinimalLocation location, SharpMeasuresVectorDefinition definition,
        IEnumerable<UnresolvedIncludeUnitsDefinition> includeUnits, IEnumerable<UnresolvedExcludeUnitsDefinition> excludeUnits,
        IEnumerable<VectorConstantDefinition> vectorConstants, IEnumerable<UnresolvedConvertibleQuantityDefinition> dimensionalEquivalences)
    {
        return new(type, location, definition, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);
    }

    private IncrementalValuesProvider<BaseVector> ConstructInterfaces()
    {
        return ProcessedProvider.Select(ConstructInterface);
    }

    private BaseVector ConstructInterface(ParsedBaseVector input, CancellationToken _)
    {
        var includedUnits = input.IncludeUnits.Select(static (x) => new IIncludeUnits(x.IncludedUnits));
        var excludedUnits = input.ExcludeUnits.Select(static (x) => new IExcludeUnits(x.ExcludedUnits));
        var dimensionalEquivalences = input.DimensionalEquivalences.Select(static (x) => new DimensionalEquivalenceInterface(x.Quantities, x.CastOperatorBehaviour));

        return new(input.VectorType.AsNamedType(), input.VectorDefinition.Unit, input.VectorDefinition.Scalar, input.VectorDefinition.Dimension,
            input.VectorDefinition.ImplementSum, input.VectorDefinition.ImplementDifference, input.VectorDefinition.Difference, input.VectorDefinition.DefaultUnitName,
            input.VectorDefinition.DefaultUnitSymbol, includedUnits, excludedUnits, dimensionalEquivalences);
    }
}
