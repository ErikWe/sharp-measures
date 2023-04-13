namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.UnitlessQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawUnitlessQuantity? Target(IConstructiveSemanticAttributeParser<IRawUnitlessQuantity> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSemanticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.AllowNegative);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AllowNegative_Match(IConstructiveSemanticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity(AllowNegative = true)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.True(actual!.AllowNegative);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSemanticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity(ImplementSum = true)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.AllowNegative);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSemanticAttributeParser<IRawUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.UnitlessQuantity(ImplementDifference = true)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.AllowNegative);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }
}
