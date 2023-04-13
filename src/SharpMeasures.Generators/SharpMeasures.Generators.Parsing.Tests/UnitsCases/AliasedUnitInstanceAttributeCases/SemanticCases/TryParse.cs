namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.AliasedUnitInstanceAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawAliasedUnitInstance? Target(IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias(null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.OriginalUnitInstance);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", "Metres", "Foot")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.UnitInstanceAlias("Metre", "Foot")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Null(actual.Syntax);
    }
}
