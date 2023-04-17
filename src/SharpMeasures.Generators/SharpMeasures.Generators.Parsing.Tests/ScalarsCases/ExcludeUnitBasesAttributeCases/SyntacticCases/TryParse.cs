namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.ExcludeUnitBasesAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawExcludeUnitBases? Target(IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedExcludedUnitBasesCollectionLocation, expectedExcludedUnitBasesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.ExcludedUnitBases);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedExcludedUnitBasesCollectionLocation, actual.Syntax.ExcludedUnitBasesCollection);
        Assert.Equal(expectedExcludedUnitBasesElementLocations, actual.Syntax.ExcludedUnitBasesElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnitBases_Match(IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedExcludedUnitBasesCollectionLocation, expectedExcludedUnitBasesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new[] { "42", null }, actual!.ExcludedUnitBases);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedExcludedUnitBasesCollectionLocation, actual.Syntax.ExcludedUnitBasesCollection);
        Assert.Equal(expectedExcludedUnitBasesElementLocations, actual.Syntax.ExcludedUnitBasesElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnitBases(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedExcludedUnitBasesCollectionLocation, expectedExcludedUnitBasesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);
        var expectedStackingModeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.ExcludedUnitBases);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedExcludedUnitBasesCollectionLocation, actual.Syntax.ExcludedUnitBasesCollection);
        Assert.Equal(expectedExcludedUnitBasesElementLocations, actual.Syntax.ExcludedUnitBasesElements);
        Assert.Equal(expectedStackingModeLocation, actual.Syntax.StackingMode);
    }
}
