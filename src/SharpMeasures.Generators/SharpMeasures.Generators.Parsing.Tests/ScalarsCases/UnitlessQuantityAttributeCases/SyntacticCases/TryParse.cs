namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.UnitlessQuantityAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawUnitlessQuantity? Target(IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AllowNegative_Match(IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity(AllowNegative = true)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAllowNegativeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.True(actual!.AllowNegative);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedAllowNegativeLocation, actual.Syntax!.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity(ImplementSum = true)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedImplementSumLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.AllowNegative);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(Location.None, actual.Syntax!.AllowNegative);
        Assert.Equal(expectedImplementSumLocation, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity(ImplementDifference = true)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedImplementDifferenceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.AllowNegative);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);

        Assert.Equal(Location.None, actual.Syntax!.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(expectedImplementDifferenceLocation, actual.Syntax.ImplementDifference);
    }
}
