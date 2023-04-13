namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.SpecializedScalarQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;
using SharpMeasures.Generators.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity>>
    {
        protected override IEnumerable<IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity>>()
        };
    }
}
