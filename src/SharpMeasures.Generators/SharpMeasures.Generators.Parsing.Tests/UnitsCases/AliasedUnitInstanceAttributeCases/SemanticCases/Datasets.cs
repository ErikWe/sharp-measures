namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.AliasedUnitInstanceAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Tests;
using SharpMeasures.Generators.Parsing.Units;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance>>
    {
        protected override IEnumerable<IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance>>()
        };
    }
}
