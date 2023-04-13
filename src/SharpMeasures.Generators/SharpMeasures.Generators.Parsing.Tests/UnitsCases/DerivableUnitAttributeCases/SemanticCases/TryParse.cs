namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.DerivableUnitAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawDerivableUnit? Target(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser)
    {
        var source = """
            [SharpMeasures.DerivableUnit(null, (string)null, null)]
            public class Foo { }
            """;

        var (c, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var d = c.GetDiagnostics();
        Assert.Empty(d);

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.DerivationID);
        Assert.Null(actual.Expression);
        Assert.Null(actual.Signature);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithDerivationID_Match(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser)
    {
        var source = """
            [SharpMeasures.DerivableUnit("ID", "Expression", new[] { typeof(int), typeof(string) })]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedSignature = new[] { intType, stringType };

        var actual = Target(parser, attributeData);

        Assert.Equal("ID", actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(expectedSignature, actual.Signature);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutDerivationID_Match(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser)
    {
        var source = """
            [SharpMeasures.DerivableUnit("Expression", new[] { typeof(int), typeof(string) })]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedSignature = new[] { intType, stringType };

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(expectedSignature, actual.Signature);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptySignature_Match(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser)
    {
        var source = """
            [SharpMeasures.DerivableUnit("Expression", new System.Type[0])]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(Array.Empty<ITypeSymbol>(), actual.Signature);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingSignature_Match(IConstructiveSemanticAttributeParser<IRawDerivableUnit> parser)
    {
        var source = """
            [SharpMeasures.DerivableUnit("Expression", new[] { typeof(int), null })]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedSignature = new[] { intType, null };

        var actual = Target(parser, attributeData);

        Assert.Null(actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(expectedSignature, actual.Signature);
        Assert.Null(actual.Syntax);
    }
}
