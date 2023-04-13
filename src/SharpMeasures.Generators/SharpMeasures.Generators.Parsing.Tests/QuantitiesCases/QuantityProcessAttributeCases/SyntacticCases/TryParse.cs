namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.QuantityProcessAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawQuantityProcess? Target(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X")]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X")]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>(null, null)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Null(actual.Name);
        Assert.Null(actual.Expression);
        Assert.Null(actual.Signature);
        Assert.Null(actual.ParameterNames);
        Assert.Null(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(Location.None, actual.Syntax.SignatureCollection);
        Assert.Empty(actual.Syntax.SignatureElements);
        Assert.Equal(Location.None, actual.Syntax.ParameterNamesCollection);
        Assert.Empty(actual.Syntax.ParameterNamesElements);
        Assert.Equal(Location.None, actual.Syntax.ImplementStatically);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>(null, null, null, null)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);
        var (expectedParameterNamesCollectionLocation, expectedParameterNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Null(actual.Name);
        Assert.Null(actual.Expression);
        Assert.Null(actual.Signature);
        Assert.Null(actual.ParameterNames);
        Assert.Null(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
        Assert.Equal(expectedParameterNamesCollectionLocation, actual.Syntax.ParameterNamesCollection);
        Assert.Equal(expectedParameterNamesElementLocations, actual.Syntax.ParameterNamesElements);
        Assert.Equal(Location.None, actual.Syntax.ImplementStatically);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X", new[] { typeof(string) }, new[] { "parameter" }, ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);
        var (expectedParameterNamesCollectionLocation, expectedParameterNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);
        var expectedImplementStaticallyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 4);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Process", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.Equal(new[] { stringType }, actual.Signature);
        Assert.Equal(new[] { "parameter" }, actual.ParameterNames);
        Assert.True(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
        Assert.Equal(expectedParameterNamesCollectionLocation, actual.Syntax.ParameterNamesCollection);
        Assert.Equal(expectedParameterNamesElementLocations, actual.Syntax.ParameterNamesElements);
        Assert.Equal(expectedImplementStaticallyLocation, actual.Syntax.ImplementStatically);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyArrays_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X", new System.Type[0], new string[0], ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);
        var (expectedParameterNamesCollectionLocation, expectedParameterNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);
        var expectedImplementStaticallyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 4);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Process", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.Empty(actual.Signature);
        Assert.Empty(actual.ParameterNames);
        Assert.True(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
        Assert.Equal(expectedParameterNamesCollectionLocation, actual.Syntax.ParameterNamesCollection);
        Assert.Equal(expectedParameterNamesElementLocations, actual.Syntax.ParameterNamesElements);
        Assert.Equal(expectedImplementStaticallyLocation, actual.Syntax.ImplementStatically);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingArrays_Match(IConstructiveSyntacticAttributeParser<IRawQuantityProcess> parser)
    {
        var source = """
            [SharpMeasures.QuantityProcess<int>("Process", "X", new System.Type[] { null }, new string[] { null }, ImplementStatically = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedExpressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedSignatureCollectionLocation, expectedSignatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);
        var (expectedParameterNamesCollectionLocation, expectedParameterNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);
        var expectedImplementStaticallyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 4);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal("Process", actual.Name);
        Assert.Equal("X", actual.Expression);
        Assert.Equal(new ITypeSymbol?[] { null }, actual.Signature);
        Assert.Equal(new string?[] { null }, actual.ParameterNames);
        Assert.True(actual.ImplementStatically);

        Assert.Equal(expectedResultLocation, actual.Syntax!.Result);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedExpressionLocation, actual.Syntax.Expression);
        Assert.Equal(expectedSignatureCollectionLocation, actual.Syntax.SignatureCollection);
        Assert.Equal(expectedSignatureElementLocations, actual.Syntax.SignatureElements);
        Assert.Equal(expectedParameterNamesCollectionLocation, actual.Syntax.ParameterNamesCollection);
        Assert.Equal(expectedParameterNamesElementLocations, actual.Syntax.ParameterNamesElements);
        Assert.Equal(expectedImplementStaticallyLocation, actual.Syntax.ImplementStatically);
    }
}
