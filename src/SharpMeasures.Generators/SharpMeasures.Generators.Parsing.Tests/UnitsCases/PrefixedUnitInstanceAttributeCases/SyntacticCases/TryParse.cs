namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.PrefixedUnitInstanceAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawPrefixedUnitInstance? Target(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Metres", "Foot", SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Metres", "Foot", SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance(null, null, null, SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedPrefixLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.OriginalUnitInstance);
        Assert.Equal(MetricPrefixName.Kilo, actual.Prefix);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedPrefixLocation, actual.Syntax.Prefix);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Metres", "Foot", SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedPrefixLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(MetricPrefixName.Kilo, actual.Prefix);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedPrefixLocation, actual.Syntax.Prefix);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Foot", SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedPrefixLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(MetricPrefixName.Kilo, actual.Prefix);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedPrefixLocation, actual.Syntax.Prefix);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BinaryPrefix_Match(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Foot", SharpMeasures.BinaryPrefixName.Kibi)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedOriginalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedPrefixLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(BinaryPrefixName.Kibi, actual.Prefix);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(expectedOriginalUnitInstanceLocation, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(expectedPrefixLocation, actual.Syntax.Prefix);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Integer_Null(IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Foot", 1)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual);
    }
}
