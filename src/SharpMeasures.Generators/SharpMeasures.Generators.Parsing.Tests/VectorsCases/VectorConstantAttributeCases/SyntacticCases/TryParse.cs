namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorConstantAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorConstant? Target(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new double[0])]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new double[0])]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValue_Match(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant(null, null, (double[])null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedValueCollectionLocation, expectedValueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Null(actual.Value.AsT0);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueCollectionLocation, actual.Syntax.ValueCollection);
        Assert.Equal(expectedValueElementLocations, actual.Syntax.ValueElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullExpressions_Match(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant(null, null, (string[])null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedValueCollectionLocation, expectedValueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Null(actual.Value.AsT1);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueCollectionLocation, actual.Syntax.ValueCollection);
        Assert.Equal(expectedValueElementLocations, actual.Syntax.ValueElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyValue_Match(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant(null, null, new double[0])]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedValueCollectionLocation, expectedValueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Empty(actual.Value.AsT0);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueCollectionLocation, actual.Syntax.ValueCollection);
        Assert.Equal(expectedValueElementLocations, actual.Syntax.ValueElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyExpressions_Match(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant(null, null, new string[0])]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedValueCollectionLocation, expectedValueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Empty(actual.Value.AsT1);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueCollectionLocation, actual.Syntax.ValueCollection);
        Assert.Equal(expectedValueElementLocations, actual.Syntax.ValueElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Value_Match(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new double[] { 42, 43 })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedValueCollectionLocation, expectedValueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Unit", actual.UnitInstanceName);
        Assert.Equal(new double[] { 42, 43 }, actual.Value.AsT0);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueCollectionLocation, actual.Syntax.ValueCollection);
        Assert.Equal(expectedValueElementLocations, actual.Syntax.ValueElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingExpressions_Match(IConstructiveSyntacticAttributeParser<IRawVectorConstant> parser)
    {
        var source = """
            [SharpMeasures.VectorConstant("Constant", "Unit", new[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedValueCollectionLocation, expectedValueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Unit", actual.UnitInstanceName);
        Assert.Equal(new[] { "42", null }, actual.Value.AsT1);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueCollectionLocation, actual.Syntax.ValueCollection);
        Assert.Equal(expectedValueElementLocations, actual.Syntax.ValueElements);
    }
}
