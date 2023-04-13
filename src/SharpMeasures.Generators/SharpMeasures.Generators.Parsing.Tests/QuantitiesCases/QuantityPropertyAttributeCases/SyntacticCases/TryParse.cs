namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.QuantityPropertyAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawQuantityProperty? Target(IConstructiveSyntacticAttributeParser<IRawQuantityProperty> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawQuantityProperty> parser)
    {
        var source = """
            [SharpMeasures.QuantityProperty<int>("Property", "X")]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawQuantityProperty> parser)
    {
        var source = """
            [SharpMeasures.QuantityProperty<int>("Property", "X")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProperty> parser)
    {
        var source = """
            [SharpMeasures.QuantityProperty<int>(null, null)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Null(actual.Name);
        Assert.Null(actual.Expression);
        Assert.Null(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(Location.None, actual.Syntax.ImplementStatically);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProperty> parser)
    {
        var source = """
            [SharpMeasures.QuantityProperty<int>("Property", "X", ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedImplementStaticallyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Property", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.True(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedImplementStaticallyLocation, actual.Syntax.ImplementStatically);
    }
}
