namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.ScalarQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScalarQuantity? Target(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AllowNegative_Match(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(AllowNegative = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.True(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task UseUnitBias_Match(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(UseUnitBias = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.True(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSemanticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }
}
