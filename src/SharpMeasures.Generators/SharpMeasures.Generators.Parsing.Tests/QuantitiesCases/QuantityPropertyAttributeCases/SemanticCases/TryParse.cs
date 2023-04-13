namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.QuantityPropertyAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawQuantityProperty? Target(IConstructiveSemanticAttributeParser<IRawQuantityProperty> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawQuantityProperty> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawQuantityProperty> parser)
    {
        var source = """
            [SharpMeasures.QuantityProperty<int>(null, null)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Null(actual.Name);
        Assert.Null(actual.Expression);
        Assert.Null(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSemanticAttributeParser<IRawQuantityProperty> parser)
    {
        var source = """
            [SharpMeasures.QuantityProperty<int>("Property", "X", ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Property", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.True(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }
}
