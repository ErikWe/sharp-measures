namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.DerivedUnitInstanceAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawDerivedUnitInstance? Target(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance(null, null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Null(actual.Units);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", "Metres", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(new[] { "42", "314" }, actual.Units);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(new[] { "42", "314" }, actual.Units);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithDerivationID_Match(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", null, "ID", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("ID", actual.DerivationID);
        Assert.Equal(new[] { "42", "314" }, actual.Units);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyUnits_Match(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", new string[0])]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(Array.Empty<string>(), actual.Units);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingUnits_Match(IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", new[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(new[] { "42", null }, actual.Units);
        Assert.Null(actual.Syntax);
    }
}
