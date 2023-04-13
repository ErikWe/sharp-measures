namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.ExcludeUnitBasesAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawExcludeUnitBases? Target(IConstructiveSemanticAttributeParser<IRawExcludeUnitBases> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.ExcludedUnitBases);
        Assert.Null(actual.StackingMode);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnitBases_Match(IConstructiveSemanticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal(new[] { "42", null }, actual!.ExcludedUnitBases);
        Assert.Null(actual.StackingMode);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSemanticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.ExcludedUnitBases);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);
        Assert.Null(actual.Syntax);
    }
}
