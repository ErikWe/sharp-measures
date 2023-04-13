namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.DerivedUnitInstanceAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;
using SharpMeasures.Generators.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance>>
    {
        protected override IEnumerable<IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance>>()
        };
    }
}
