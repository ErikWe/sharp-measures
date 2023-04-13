namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorComponentNamesAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorComponentNames? Target(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullNames_Match(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames(null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedNamesCollectionLocation, expectedNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Names);

        Assert.Equal(expectedNamesCollectionLocation, actual.Syntax!.NamesCollection);
        Assert.Equal(expectedNamesElementLocations, actual.Syntax.NamesElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyNames_Match(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Empty(actual!.Names);

        Assert.Equal(Location.None, actual.Syntax!.NamesCollection);
        Assert.Empty(actual.Syntax.NamesElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingNames_Match(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames(new string[] { "X", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedNamesCollectionLocation, expectedNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new[] { "X", null }, actual!.Names);

        Assert.Equal(expectedNamesCollectionLocation, actual.Syntax!.NamesCollection);
        Assert.Equal(expectedNamesElementLocations, actual.Syntax.NamesElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSyntacticAttributeParser<IRawVectorComponentNames> parser)
    {
        var source = """
            [SharpMeasures.VectorComponentNames(new[] { "X", "Y" })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var (expectedNamesCollectionLocation, expectedNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new[] { "X", "Y" }, actual!.Names);

        Assert.Equal(expectedNamesCollectionLocation, actual.Syntax!.NamesCollection);
        Assert.Equal(expectedNamesElementLocations, actual.Syntax.NamesElements);
    }
}
