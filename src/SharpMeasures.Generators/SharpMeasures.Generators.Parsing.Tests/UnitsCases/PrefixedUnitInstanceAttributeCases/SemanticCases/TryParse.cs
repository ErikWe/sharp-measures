namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.PrefixedUnitInstanceAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawPrefixedUnitInstance? Target(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance(null, null, null, SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.OriginalUnitInstance);
        Assert.Equal(MetricPrefixName.Kilo, actual.Prefix);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Metres", "Foot", SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(MetricPrefixName.Kilo, actual.Prefix);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Foot", SharpMeasures.MetricPrefixName.Kilo)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(MetricPrefixName.Kilo, actual.Prefix);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BinaryPrefix_Match(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Foot", SharpMeasures.BinaryPrefixName.Kibi)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("Foot", actual.OriginalUnitInstance);
        Assert.Equal(BinaryPrefixName.Kibi, actual.Prefix);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Integer_Null(IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.PrefixedUnitInstance("Metre", "Foot", 1)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual);
    }
}
