namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.UnitAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawUnit? Target(IConstructiveSyntacticAttributeParser<IRawUnit> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSyntacticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(expectedType, actual!.Scalar);
        Assert.False(actual.BiasTerm.HasValue);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedScalarLocation, actual.Syntax.Scalar);
        Assert.Equal(Location.None, actual.Syntax.BiasTerm);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BiasTrue_Match(IConstructiveSyntacticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>(BiasTerm = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedBiasTermLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(expectedType, actual!.Scalar);
        Assert.True(actual.BiasTerm);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedScalarLocation, actual.Syntax.Scalar);
        Assert.Equal(expectedBiasTermLocation, actual.Syntax.BiasTerm);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BiasFalse_Match(IConstructiveSyntacticAttributeParser<IRawUnit> parser)
    {
        var source = """
            [SharpMeasures.Unit<int>(BiasTerm = false)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedBiasTermLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(expectedType, actual!.Scalar);
        Assert.False(actual.BiasTerm);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedScalarLocation, actual.Syntax.Scalar);
        Assert.Equal(expectedBiasTermLocation, actual.Syntax.BiasTerm);
    }
}
