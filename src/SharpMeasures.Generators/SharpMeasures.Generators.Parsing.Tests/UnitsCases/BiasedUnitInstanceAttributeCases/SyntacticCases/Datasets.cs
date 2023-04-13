namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.BiasedUnitInstanceAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Tests;
using SharpMeasures.Generators.Parsing.Units;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;
    public static AttributeSyntax GetNullAttributeSyntax() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance>>
    {
        protected override IEnumerable<IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance>>()
        };
    }
}
