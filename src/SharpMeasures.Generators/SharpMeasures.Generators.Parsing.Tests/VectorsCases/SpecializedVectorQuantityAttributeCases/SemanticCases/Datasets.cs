namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.SpecializedVectorQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;
using SharpMeasures.Generators.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity>>
    {
        protected override IEnumerable<IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity>>()
        };
    }
}
