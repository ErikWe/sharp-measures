namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.UnitAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawUnit? Target(IConstructiveSemanticAttributeParser<IRawUnit> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawUnit> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSemanticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var expectedType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(expectedType, actual!.Scalar);
        Assert.Null(actual.BiasTerm);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BiasTrue_Match(IConstructiveSemanticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>(BiasTerm = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var expectedType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(expectedType, actual!.Scalar);
        Assert.True(actual.BiasTerm);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BiasFalse_Match(IConstructiveSemanticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>(BiasTerm = false)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var expectedType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(expectedType, actual!.Scalar);
        Assert.False(actual.BiasTerm);
        Assert.Null(actual.Syntax);
    }
}
