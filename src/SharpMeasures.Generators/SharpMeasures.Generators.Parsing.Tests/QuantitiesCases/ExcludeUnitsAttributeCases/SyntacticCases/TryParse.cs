namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.ExcludeUnitsAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawExcludeUnits? Target(IConstructiveSyntacticAttributeParser<IRawExcludeUnits> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawExcludeUnits> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnits(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawExcludeUnits> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnits(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawExcludeUnits> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnits(null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedExcludedUnitsCollectionLocation, expectedExcludedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.ExcludedUnits);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedExcludedUnitsCollectionLocation, actual.Syntax.ExcludedUnitsCollection);
        Assert.Equal(expectedExcludedUnitsElementLocations, actual.Syntax.ExcludedUnitsElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnitBases_Match(IConstructiveSyntacticAttributeParser<IRawExcludeUnits> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnits(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedExcludedUnitsCollectionLocation, expectedExcludedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new[] { "42", null }, actual!.ExcludedUnits);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedExcludedUnitsCollectionLocation, actual.Syntax.ExcludedUnitsCollection);
        Assert.Equal(expectedExcludedUnitsElementLocations, actual.Syntax.ExcludedUnitsElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSyntacticAttributeParser<IRawExcludeUnits> parser)
    {
        var source = """
            [SharpMeasures.ExcludeUnits(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedExcludedUnitsCollectionLocation, expectedExcludedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);
        var expectedStackingModeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.ExcludedUnits);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedExcludedUnitsCollectionLocation, actual.Syntax.ExcludedUnitsCollection);
        Assert.Equal(expectedExcludedUnitsElementLocations, actual.Syntax.ExcludedUnitsElements);
        Assert.Equal(expectedStackingModeLocation, actual.Syntax.StackingMode);
    }
}
