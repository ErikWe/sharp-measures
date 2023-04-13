namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.ScalarConstantAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScalarConstant? Target(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant(null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Null(actual.Value.AsT1);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Value_Match(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal(42, actual.Value);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Expression_Match(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", "42")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal("42", actual.Value);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NamedProperties_Match(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42, GenerateMultiplesProperty = true, MultiplesName = "Constants")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal(42, actual.Value);
        Assert.True(actual.GenerateMultiplesProperty);
        Assert.Equal("Constants", actual.MultiplesName);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task MultiplesName_ExplicitlyNull_Match(IConstructiveSemanticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42, MultiplesName = null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal(42, actual.Value);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);
        Assert.Null(actual.Syntax);
    }
}
