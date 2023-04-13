namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.QuantityProcessAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawQuantityProcess? Target(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>(null, null)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Null(actual.Name);
        Assert.Null(actual.Expression);
        Assert.Null(actual.Signature);
        Assert.Null(actual.ParameterNames);
        Assert.Null(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>(null, null, null, null)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Null(actual.Name);
        Assert.Null(actual.Expression);
        Assert.Null(actual.Signature);
        Assert.Null(actual.ParameterNames);
        Assert.Null(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X", new[] { typeof(string) }, new[] { "parameter" }, ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Process", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.Equal(new[] { stringType }, actual.Signature);
        Assert.Equal(new[] { "parameter" }, actual.ParameterNames);
        Assert.True(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyArrays_Match(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X", new System.Type[0], new string[0], ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Process", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.Empty(actual.Signature);
        Assert.Empty(actual.ParameterNames);
        Assert.True(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingArrays_Match(IConstructiveSemanticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X", new System.Type[] { null }, new string[] { null }, ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Process", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.Equal(new ITypeSymbol?[] { null }, actual.Signature);
        Assert.Equal(new string?[] { null }, actual.ParameterNames);
        Assert.True(actual.ImplementStatically);
        Assert.Null(actual.Syntax);
    }
}
