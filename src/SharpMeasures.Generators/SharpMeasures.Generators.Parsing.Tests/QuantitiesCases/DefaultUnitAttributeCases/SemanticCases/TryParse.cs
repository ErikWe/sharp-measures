namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.DefaultUnitAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawDefaultUnit? Target(IConstructiveSemanticAttributeParser<IRawDefaultUnit> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawDefaultUnit> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawDefaultUnit> parser)
    {
        var source = """
            [SharpMeasures.DefaultUnit(null, null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Unit);
        Assert.Null(actual.Symbol);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSemanticAttributeParser<IRawDefaultUnit> parser)
    {
        var source = """
            [SharpMeasures.DefaultUnit("Unit", "Symbol")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Unit", actual!.Unit);
        Assert.Equal("Symbol", actual.Symbol);
        Assert.Null(actual.Syntax);
    }
}
