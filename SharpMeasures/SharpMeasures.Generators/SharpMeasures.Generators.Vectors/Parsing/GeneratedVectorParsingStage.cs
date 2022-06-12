namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class GeneratedVectorParsingStage : AVectorParsingStage<GeneratedVectorAttribute, RawGeneratedVectorDefinition, GeneratedVectorDefinition,
    RawParsedGeneratedVector, ParsedGeneratedVector>
{
    public IncrementalValuesProvider<GeneratedVectorInterface> InterfaceProvider { get; }

    public GeneratedVectorParsingStage(IncrementalGeneratorInitializationContext context) : base(context)
    {
        InterfaceProvider = ConstructInterfaces();
    }

    protected override IProcesser<IProcessingContext, RawGeneratedVectorDefinition, GeneratedVectorDefinition> Processer
        => new GeneratedVectorProcesser(GeneratedVectorDiagnostics.Instance);

    protected override RawGeneratedVectorDefinition? Parse(INamedTypeSymbol typeSymbol) => GeneratedVectorParser.Parser.ParseFirstOccurrence(typeSymbol);

    protected override RawParsedGeneratedVector ConstructParsed(DefinedType type, MinimalLocation location, GeneratedVectorDefinition definition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawVectorConstantDefinition> vectorConstants, IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalences)
    {
        return new(type, location, definition, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);
    }

    protected override ParsedGeneratedVector ConstructProcessed(DefinedType type, MinimalLocation location, GeneratedVectorDefinition definition,
        IEnumerable<IncludeUnitsDefinition> includeUnits, IEnumerable<ExcludeUnitsDefinition> excludeUnits,
        IEnumerable<VectorConstantDefinition> vectorConstants, IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalences)
    {
        return new(type, location, definition, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);
    }

    private IncrementalValuesProvider<GeneratedVectorInterface> ConstructInterfaces()
    {
        return ProcessedProvider.Select(ConstructInterface);
    }

    private GeneratedVectorInterface ConstructInterface(ParsedGeneratedVector input, CancellationToken _)
    {
        var includedUnits = input.IncludeUnits.Select(static (x) => new IncludeUnitsInterface(x.IncludedUnits));
        var excludedUnits = input.ExcludeUnits.Select(static (x) => new ExcludeUnitsInterface(x.ExcludedUnits));
        var dimensionalEquivalences = input.DimensionalEquivalences.Select(static (x) => new DimensionalEquivalenceInterface(x.Quantities, x.CastOperatorBehaviour));

        return new(input.VectorType.AsNamedType(), input.VectorDefinition.Unit, input.VectorDefinition.Scalar, input.VectorDefinition.Dimension,
            input.VectorDefinition.DefaultUnitName, input.VectorDefinition.DefaultUnitSymbol, includedUnits, excludedUnits, dimensionalEquivalences);
    }
}
