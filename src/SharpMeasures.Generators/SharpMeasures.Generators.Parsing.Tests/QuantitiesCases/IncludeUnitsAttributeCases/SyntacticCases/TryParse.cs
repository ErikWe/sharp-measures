namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.IncludeUnitsAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawIncludeUnits? Target(IConstructiveSyntacticAttributeParser<IRawIncludeUnits> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(new[] { "1", "2" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedIncludedUnitsCollectionLocation, expectedIncludedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.IncludedUnits);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedIncludedUnitsCollectionLocation, actual.Syntax!.IncludedUnitsCollection);
        Assert.Equal(expectedIncludedUnitsElementLocations, actual.Syntax.IncludedUnitsElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnits_Match(IConstructiveSyntacticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(new string[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedIncludedUnitsCollectionLocation, expectedIncludedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new[] { "42", null }, actual!.IncludedUnits);
        Assert.Null(actual.StackingMode);

        Assert.Equal(expectedIncludedUnitsCollectionLocation, actual.Syntax!.IncludedUnitsCollection);
        Assert.Equal(expectedIncludedUnitsElementLocations, actual.Syntax.IncludedUnitsElements);
        Assert.Equal(Location.None, actual.Syntax.StackingMode);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task StackingMode_Match(IConstructiveSyntacticAttributeParser<IRawIncludeUnits> parser)
    {
        var source = """
            [SharpMeasures.IncludeUnits(null, StackingMode = SharpMeasures.FilterStackingMode.Keep)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedIncludedUnitsCollectionLocation, expectedIncludedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);
        var expectedStackingModeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.IncludedUnits);
        Assert.Equal(FilterStackingMode.Keep, actual.StackingMode);

        Assert.Equal(expectedIncludedUnitsCollectionLocation, actual.Syntax!.IncludedUnitsCollection);
        Assert.Equal(expectedIncludedUnitsElementLocations, actual.Syntax.IncludedUnitsElements);
        Assert.Equal(expectedStackingModeLocation, actual.Syntax.StackingMode);
    }
}
