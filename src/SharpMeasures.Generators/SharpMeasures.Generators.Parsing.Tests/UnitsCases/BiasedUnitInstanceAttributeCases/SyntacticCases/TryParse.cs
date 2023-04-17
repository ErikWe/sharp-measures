namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.BiasedUnitInstanceAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawBiasedUnitInstance? Target(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.BiasedUnitInstance("Metre", "Metres", "Foot", 42)]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.BiasedUnitInstance("Metre", "Metres", "Foot", 42)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.BiasedUnitInstance(null, null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedBiasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.OriginalUnitInstance);
        Assert.Null(actual.Bias.AsT1);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedBiasLocation, actual.Syntax.Bias);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.BiasedUnitInstance("Metre", "Metres", "Foot", 42)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedBiasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(42, actual.Bias);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedBiasLocation, actual.Syntax.Bias);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.BiasedUnitInstance("Metre", "Foot", 42)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedBiasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(42, actual.Bias);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedBiasLocation, actual.Syntax.Bias);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Expression_Match(IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.BiasedUnitInstance("Metre", "Foot", "42")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedBiasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal("42", actual.Bias);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedBiasLocation, actual.Syntax.Bias);
    }
}
