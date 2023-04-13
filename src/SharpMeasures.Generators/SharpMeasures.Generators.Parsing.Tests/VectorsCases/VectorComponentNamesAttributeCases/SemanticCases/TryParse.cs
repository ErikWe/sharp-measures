namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorComponentNamesAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorComponentNames? Target(IConstructiveSemanticAttributeParser<IRawVectorComponentNames> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawVectorComponentNames> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullNames_Match(IConstructiveSemanticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames(null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Names);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyNames_Match(IConstructiveSemanticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Empty(actual!.Names);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingNames_Match(IConstructiveSemanticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames(new[] { "X", null })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal(new[] { "X", null }, actual!.Names);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSemanticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames(new[] { "X", "Y" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal(new[] { "X", "Y" }, actual!.Names);
        Assert.Null(actual.Syntax);
    }
}
