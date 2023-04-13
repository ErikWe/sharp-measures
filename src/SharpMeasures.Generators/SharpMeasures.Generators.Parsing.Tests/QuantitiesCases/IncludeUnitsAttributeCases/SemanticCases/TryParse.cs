namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.IncludeUnitsAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawIncludeUnits? Target(IConstructiveSemanticAttributeParser<IRawIncludeUnits> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawIncludeUnits> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.IncludedUnits);
        Assert.Null(actual.StackingMode);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingIncludedUnits_Match(IConstructiveSemanticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal(new[] { "42", null }, actual!.IncludedUnits);
        Assert.Null(actual.StackingMode);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSemanticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.IncludedUnits);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);
        Assert.Null(actual.Syntax);
    }
}
