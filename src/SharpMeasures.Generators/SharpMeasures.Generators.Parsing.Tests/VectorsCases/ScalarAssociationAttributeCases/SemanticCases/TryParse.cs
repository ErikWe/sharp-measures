namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.ScalarAssociationAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScalarAssociation? Target(IConstructiveSemanticAttributeParser<IRawScalarAssociation> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawScalarAssociation> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Scalar);
        Assert.Null(actual.AsComponents);
        Assert.Null(actual.AsMagnitude);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AsComponents_Match(IConstructiveSemanticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>(AsComponents = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Scalar);
        Assert.True(actual.AsComponents);
        Assert.Null(actual.AsMagnitude);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AsMagnitude_Match(IConstructiveSemanticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>(AsMagnitude = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Scalar);
        Assert.Null(actual.AsComponents);
        Assert.True(actual.AsMagnitude);
        Assert.Null(actual.Syntax);
    }
}
