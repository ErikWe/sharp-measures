namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.ScaledUnitInstanceAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScaledUnitInstance? Target(IConstructiveSemanticAttributeParser<IRawScaledUnitInstance> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawScaledUnitInstance> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawScaledUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.ScaledUnitInstance(null, null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.OriginalUnitInstance);
        Assert.Null(actual.Scale.AsT1);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSemanticAttributeParser<IRawScaledUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.ScaledUnitInstance("Metre", "Metres", "Foot", 42)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(42, actual.Scale);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSemanticAttributeParser<IRawScaledUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.ScaledUnitInstance("Metre", "Foot", 42)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(42, actual.Scale);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Expression_Match(IConstructiveSemanticAttributeParser<IRawScaledUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.ScaledUnitInstance("Metre", "Foot", "42")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal("42", actual.Scale);
        Assert.Null(actual.Syntax);
    }
}
