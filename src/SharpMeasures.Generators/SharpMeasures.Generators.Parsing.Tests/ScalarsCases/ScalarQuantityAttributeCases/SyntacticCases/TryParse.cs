namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.ScalarQuantityAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScalarQuantity? Target(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedUnitLocation, actual.Syntax.Unit);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.UseUnitBias);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AllowNegative_Match(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(AllowNegative = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedAllowNegativeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.True(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedUnitLocation, actual.Syntax.Unit);
        Assert.Equal(expectedAllowNegativeLocation, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.UseUnitBias);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task UseUnitBias_Match(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(UseUnitBias = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedUseUnitBiasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.True(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedUnitLocation, actual.Syntax.Unit);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(expectedUseUnitBiasLocation, actual.Syntax.UseUnitBias);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementSumLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedUnitLocation, actual.Syntax.Unit);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.UseUnitBias);
        Assert.Equal(expectedImplementSumLocation, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSyntacticAttributeParser<IRawScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.ScalarQuantity<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedUnitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementDifferenceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Unit);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.UseUnitBias);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedUnitLocation, actual.Syntax.Unit);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.UseUnitBias);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(expectedImplementDifferenceLocation, actual.Syntax.ImplementDifference);
    }
}
