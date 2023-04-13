namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.DefaultUnitAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawDefaultUnit? Target(IConstructiveSyntacticAttributeParser<IRawDefaultUnit> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawDefaultUnit> parser)
    {
        var source = """
            [SharpMeasures.DefaultUnit("Unit", "Symbol")]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawDefaultUnit> parser)
    {
        var source = """
            [SharpMeasures.DefaultUnit("Unit", "Symbol")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawDefaultUnit> parser)
    {
        var source = """
            [SharpMeasures.DefaultUnit(null, null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedUnitLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedSymbolLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Unit);
        Assert.Null(actual.Symbol);

        Assert.Equal(expectedUnitLocation, actual.Syntax!.Unit);
        Assert.Equal(expectedSymbolLocation, actual.Syntax.Symbol);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSyntacticAttributeParser<IRawDefaultUnit> parser)
    {
        var source = """
            [SharpMeasures.DefaultUnit("Unit", "Symbol")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedUnitLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedSymbolLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Unit", actual!.Unit);
        Assert.Equal("Symbol", actual.Symbol);

        Assert.Equal(expectedUnitLocation, actual.Syntax!.Unit);
        Assert.Equal(expectedSymbolLocation, actual.Syntax.Symbol);
    }
}
