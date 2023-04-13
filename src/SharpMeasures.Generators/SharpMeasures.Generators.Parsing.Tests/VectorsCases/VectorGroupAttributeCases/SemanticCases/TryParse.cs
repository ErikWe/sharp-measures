namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorGroupAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorGroup? Target(IConstructiveSemanticAttributeParser<IRawVectorGroup> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawVectorGroup> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSemanticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSemanticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }
}
