namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.DerivedUnitInstanceAttributeCases.SyntacticCases;

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
    public sealed class ParserSources : ATestDataset<IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance>>
    {
        protected override IEnumerable<IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance>>()
        };
    }
}
