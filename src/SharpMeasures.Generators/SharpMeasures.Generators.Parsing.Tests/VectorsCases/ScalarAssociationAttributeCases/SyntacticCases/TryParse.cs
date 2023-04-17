namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.ScalarAssociationAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScalarAssociation? Target(IConstructiveSyntacticAttributeParser<IRawScalarAssociation> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Scalar);
        Assert.Null(actual.AsComponents);
        Assert.Null(actual.AsMagnitude);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedScalarLocation, actual.Syntax.Scalar);
        Assert.Equal(Location.None, actual.Syntax.AsComponents);
        Assert.Equal(Location.None, actual.Syntax.AsMagnitude);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AsComponents_Match(IConstructiveSyntacticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>(AsComponents = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedAsComponentsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Scalar);
        Assert.True(actual.AsComponents);
        Assert.Null(actual.AsMagnitude);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedScalarLocation, actual.Syntax.Scalar);
        Assert.Equal(expectedAsComponentsLocation, actual.Syntax.AsComponents);
        Assert.Equal(Location.None, actual.Syntax.AsMagnitude);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AsMagnitude_Match(IConstructiveSyntacticAttributeParser<IRawScalarAssociation> parser)
    {
        var source = """
            [SharpMeasures.ScalarAssociation<int>(AsMagnitude = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedAsMagnitudeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Scalar);
        Assert.Null(actual.AsComponents);
        Assert.True(actual.AsMagnitude);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedScalarLocation, actual.Syntax.Scalar);
        Assert.Equal(Location.None, actual.Syntax.AsComponents);
        Assert.Equal(expectedAsMagnitudeLocation, actual.Syntax.AsMagnitude);
    }
}
