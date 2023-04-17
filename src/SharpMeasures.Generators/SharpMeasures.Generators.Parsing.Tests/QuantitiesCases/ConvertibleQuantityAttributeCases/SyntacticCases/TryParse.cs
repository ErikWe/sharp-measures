namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.ConvertibleQuantityAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawConvertibleQuantity? Target(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Empty(actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(Location.None, actual.Syntax.QuantitiesCollection);
        Assert.Empty(actual.Syntax.QuantitiesElements);
        Assert.Equal(Location.None, actual.Syntax.ForwardsBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsBehaviour);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedQuantitiesCollectionLocation, expectedQuantitiesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedQuantitiesCollectionLocation, actual.Syntax.QuantitiesCollection);
        Assert.Equal(expectedQuantitiesElementLocations, actual.Syntax.QuantitiesElements);
        Assert.Equal(Location.None, actual.Syntax.ForwardsBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsBehaviour);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnitBases_Match(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(new System.Type[] { typeof(int), null })]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var (expectedQuantitiesCollectionLocation, expectedQuantitiesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(new ITypeSymbol?[] { intType, null }, actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedQuantitiesCollectionLocation, actual.Syntax.QuantitiesCollection);
        Assert.Equal(expectedQuantitiesElementLocations, actual.Syntax.QuantitiesElements);
        Assert.Equal(Location.None, actual.Syntax.ForwardsBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsBehaviour);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ForwardsBehaviour_Match(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(ForwardsBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedForwardsBehaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Empty(actual!.Quantities);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(Location.None, actual.Syntax.QuantitiesCollection);
        Assert.Empty(actual.Syntax.QuantitiesElements);
        Assert.Equal(expectedForwardsBehaviourLocation, actual.Syntax.ForwardsBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsBehaviour);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BackwardsBehaviour_Match(IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(BackwardsBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedBackwardsBehaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Empty(actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.BackwardsBehaviour);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(Location.None, actual.Syntax.QuantitiesCollection);
        Assert.Empty(actual.Syntax.QuantitiesElements);
        Assert.Equal(Location.None, actual.Syntax.ForwardsBehaviour);
        Assert.Equal(expectedBackwardsBehaviourLocation, actual.Syntax.BackwardsBehaviour);
    }
}
