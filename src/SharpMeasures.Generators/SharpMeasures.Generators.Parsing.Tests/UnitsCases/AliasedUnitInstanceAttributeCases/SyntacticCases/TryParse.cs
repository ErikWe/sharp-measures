namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.AliasedUnitInstanceAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawAliasedUnitInstance? Target(IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", "Metres", "Foot")]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", "Metres", "Foot")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task PluralForm_Match(IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", "Metres", "Foot")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", null, "Foot")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", "Foot")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
    }
}
