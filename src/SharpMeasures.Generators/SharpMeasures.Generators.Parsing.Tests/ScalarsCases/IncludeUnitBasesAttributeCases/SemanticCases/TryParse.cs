namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.IncludeUnitBasesAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawIncludeUnitBases? Target(IConstructiveSemanticAttributeParser<IRawIncludeUnitBases> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.IncludedUnitBases);
        Assert.Null(actual.StackingMode);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingIncludedUnitBases_Match(IConstructiveSemanticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal(new[] { "42", null }, actual!.IncludedUnitBases);
        Assert.Null(actual.StackingMode);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSemanticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.IncludedUnitBases);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);
        Assert.Null(actual.Syntax);
    }
}
