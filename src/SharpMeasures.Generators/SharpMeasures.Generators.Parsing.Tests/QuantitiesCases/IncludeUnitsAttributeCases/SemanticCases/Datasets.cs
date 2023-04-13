﻿namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.IncludeUnitsAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;
using SharpMeasures.Generators.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal static class Datasets
{
    public static AttributeData GetNullAttributeData() => null!;

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ParserSources : ATestDataset<IConstructiveSemanticAttributeParser<IRawIncludeUnits>>
    {
        protected override IEnumerable<IConstructiveSemanticAttributeParser<IRawIncludeUnits>> GetSamples() => new[]
        {
            DependencyInjection.GetRequiredService<IConstructiveSemanticAttributeParser<IRawIncludeUnits>>()
        };
    }
}
