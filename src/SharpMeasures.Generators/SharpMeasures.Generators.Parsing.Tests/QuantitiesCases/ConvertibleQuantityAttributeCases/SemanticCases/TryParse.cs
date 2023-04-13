namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.ConvertibleQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawConvertibleQuantity? Target(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Empty(actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExcludedUnitBases_Match(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(new System.Type[] { typeof(int), null })]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(new ITypeSymbol?[] { intType, null }, actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ForwardsBehaviour_Match(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(ForwardsBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Empty(actual!.Quantities);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.ForwardsBehaviour);
        Assert.Null(actual.BackwardsBehaviour);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BackwardsBehaviour_Match(IConstructiveSemanticAttributeParser<IRawConvertibleQuantity> parser)
    {
        var source = """
            [SharpMeasures.ConvertibleQuantity(BackwardsBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Empty(actual!.Quantities);
        Assert.Null(actual.ForwardsBehaviour);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.BackwardsBehaviour);
        Assert.Null(actual.Syntax);
    }
}
