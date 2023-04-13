namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorConstantAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorConstant? Target(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValue_Match(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant(null, null, (double[])null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Null(actual.Value.AsT0);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullExpressions_Match(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant(null, null, (string[])null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Null(actual.Value.AsT1);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyValue_Match(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new double[0])]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Unit", actual.UnitInstanceName);
        Assert.Empty(actual.Value.AsT0);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyExpressions_Match(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new string[0])]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Unit", actual.UnitInstanceName);
        Assert.Empty(actual.Value.AsT1);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Value_Match(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new double[] { 42 })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal(new double[] { 42 }, actual!.Value.AsT0);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExpression_Match(IConstructiveSemanticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Unit", actual.UnitInstanceName);
        Assert.Equal(new[] { "42", null }, actual.Value.AsT1);
        Assert.Null(actual.Syntax);
    }
}
