namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorGroupAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorGroup? Target(IConstructiveSyntacticAttributeParser<IRawVectorGroup> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedUnitLocation, actual.Syntax!.Unit);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementSumLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedUnitLocation, actual.Syntax!.Unit);
        Assert.Equal(expectedImplementSumLocation, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.VectorGroup<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementDifferenceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);

        Assert.Equal(expectedUnitLocation, actual.Syntax!.Unit);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(expectedImplementDifferenceLocation, actual.Syntax.ImplementDifference);
    }
}
