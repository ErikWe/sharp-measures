namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.IncludeUnitBasesAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawIncludeUnitBases? Target(IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedIncludedUnitBasesCollectionLocation, expectedIncludedUnitBasesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.IncludedUnitBases);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedIncludedUnitBasesCollectionLocation, actual.Syntax!.IncludedUnitBasesCollection);
        Assert.Equal(expectedIncludedUnitBasesElementLocations, actual.Syntax.IncludedUnitBasesElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnitBases_Match(IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedIncludedUnitBasesCollectionLocation, expectedIncludedUnitBasesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new[] { "42", null }, actual!.IncludedUnitBases);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedIncludedUnitBasesCollectionLocation, actual.Syntax!.IncludedUnitBasesCollection);
        Assert.Equal(expectedIncludedUnitBasesElementLocations, actual.Syntax.IncludedUnitBasesElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnitBases(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedIncludedUnitBasesCollectionLocation, expectedIncludedUnitBasesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);
        var expectedStackingModeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.IncludedUnitBases);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);

        Assert.Equal(expectedIncludedUnitBasesCollectionLocation, actual.Syntax!.IncludedUnitBasesCollection);
        Assert.Equal(expectedIncludedUnitBasesElementLocations, actual.Syntax.IncludedUnitBasesElements);
        Assert.Equal(expectedStackingModeLocation, actual.Syntax.StackingMode);
    }
}
