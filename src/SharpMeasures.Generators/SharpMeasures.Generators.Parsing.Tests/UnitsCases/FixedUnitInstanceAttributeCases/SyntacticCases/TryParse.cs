namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.FixedUnitInstanceAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawFixedUnitInstance? Target(IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.FixedUnitInstance("Metre", "Metres")]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.FixedUnitInstance("Metre", "Metres")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.FixedUnitInstance(null, null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.FixedUnitInstance("Metre", "Metres")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.FixedUnitInstance("Metre")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
    }
}
