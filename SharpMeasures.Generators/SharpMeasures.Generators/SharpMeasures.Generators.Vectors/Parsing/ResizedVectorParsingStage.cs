﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;
using SharpMeasures.Generators.Vectors.Parsing.ResizedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class ResizedVectorParsingStage : AVectorParsingStage<ResizedSharpMeasuresVectorAttribute, RawResizedVectorDefinition, ResizedVectorDefinition,
    RawParsedResizedVector, ParsedResizedVector>
{
    public IncrementalValuesProvider<ResizedVectorInterface> InterfaceProvider { get; }

    public ResizedVectorParsingStage(IncrementalGeneratorInitializationContext context) : base(context)
    {
        InterfaceProvider = ConstructInterfaces();
    }

    protected override IProcesser<IProcessingContext, RawResizedVectorDefinition, ResizedVectorDefinition> Processer
        => new ResizedVectorProcesser(ResizedVectorDiagnostics.Instance);

    protected override RawResizedVectorDefinition? Parse(INamedTypeSymbol typeSymbol) => ResizedVectorParser.Parser.ParseFirstOccurrence(typeSymbol);

    protected override RawParsedResizedVector ConstructParsed(DefinedType type, MinimalLocation location, ResizedVectorDefinition definition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawVectorConstantDefinition> vectorConstants, IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalences)
    {
        return new(type, location, definition, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);
    }

    protected override ParsedResizedVector ConstructProcessed(DefinedType type, MinimalLocation location, ResizedVectorDefinition definition,
        IEnumerable<IncludeUnitsDefinition> includeUnits, IEnumerable<ExcludeUnitsDefinition> excludeUnits,
        IEnumerable<VectorConstantDefinition> vectorConstants, IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalences)
    {
        return new(type, location, definition, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);
    }

    private IncrementalValuesProvider<ResizedVectorInterface> ConstructInterfaces()
    {
        return ProcessedProvider.Select(ConstructInterface);
    }

    private ResizedVectorInterface ConstructInterface(ParsedResizedVector input, CancellationToken _)
    {
        var includedUnits = input.IncludeUnits.Select(static (x) => new IncludeUnitsInterface(x.IncludedUnits));
        var excludedUnits = input.ExcludeUnits.Select(static (x) => new ExcludeUnitsInterface(x.ExcludedUnits));
        var dimensionalEquivalences = input.DimensionalEquivalences.Select(static (x) => new DimensionalEquivalenceInterface(x.Quantities, x.CastOperatorBehaviour));

        return new(input.VectorType.AsNamedType(), input.VectorDefinition.AssociatedVector, input.VectorDefinition.Dimension, includedUnits,
            excludedUnits, dimensionalEquivalences);
    }
}