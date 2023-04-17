namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.UnitDerivationAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawUnitDerivation? Target(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation("Expression", new Type[0])]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation("Expression", new Type[0])]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation(null, (string)null, null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedDerivationIDLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.DerivationID);
        Assert.Null(actual.Expression);
        Assert.Null(actual.Signature);

        Assert.Equal(expectedDerivationIDLocation, actual.Syntax!.DerivationID);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithDerivationID_Match(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation("ID", "Expression", new[] { typeof(int), typeof(string) })]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedSignature = new[] { intType, stringType };

        var expectedDerivationIDLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("ID", actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(expectedSignature, actual.Signature);

        Assert.Equal(expectedDerivationIDLocation, actual.Syntax!.DerivationID);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutDerivationID_Match(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation("Expression", new[] { typeof(int), typeof(string) })]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedSignature = new[] { intType, stringType };

        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(expectedSignature, actual.Signature);

        Assert.Equal(Location.None, actual.Syntax!.DerivationID);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptySignature_Match(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation("Expression", new System.Type[0])]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(Array.Empty<ITypeSymbol>(), actual.Signature);

        Assert.Equal(Location.None, actual.Syntax!.DerivationID);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingSignature_Match(IConstructiveSyntacticAttributeParser<IRawUnitDerivation> parser)
    {
        var source = """
            [SharpMeasures.UnitDerivation("Expression", new[] { typeof(int), null })]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedSignature = new[] { intType, null };

        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.DerivationID);
        Assert.Equal("Expression", actual.Expression);
        Assert.Equal(expectedSignature, actual.Signature);

        Assert.Equal(expectedExpressionLocation, actual.Syntax!.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
    }
}
