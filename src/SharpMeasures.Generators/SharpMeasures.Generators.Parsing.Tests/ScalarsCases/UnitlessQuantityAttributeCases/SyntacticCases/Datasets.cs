namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.UnitlessQuantityAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;
using SharpMeasures.Generators.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;
    public static AttributeSyntax GetNullAttributeSyntax() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity>>
    {
        protected override IEnumerable<IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity>>()
        };
    }
}
