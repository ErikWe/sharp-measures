namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.IncludeUnitsAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;
using SharpMeasures.Generators.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;
    public static AttributeSyntax GetNullAttributeSyntax() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSyntacticAttributeParser<IRawIncludeUnits>>
    {
        protected override IEnumerable<IConstructiveSyntacticAttributeParser<IRawIncludeUnits>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSyntacticAttributeParser<IRawIncludeUnits>>()
        };
    }
}
