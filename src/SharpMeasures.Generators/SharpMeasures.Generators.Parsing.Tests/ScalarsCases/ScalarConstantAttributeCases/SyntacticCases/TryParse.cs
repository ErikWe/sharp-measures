namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.ScalarConstantAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawScalarConstant? Target(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42)]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant(null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedValueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.UnitInstanceName);
        Assert.Null(actual.Value.AsT1);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueLocation, actual.Syntax.Value);
        Assert.Equal(Location.None, actual.Syntax.GenerateMultiplesProperty);
        Assert.Equal(Location.None, actual.Syntax.MultiplesName);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Value_Match(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedValueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal(42, actual.Value);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueLocation, actual.Syntax.Value);
        Assert.Equal(Location.None, actual.Syntax.GenerateMultiplesProperty);
        Assert.Equal(Location.None, actual.Syntax.MultiplesName);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Expression_Match(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", "42")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedValueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal("42", actual.Value);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueLocation, actual.Syntax.Value);
        Assert.Equal(Location.None, actual.Syntax.GenerateMultiplesProperty);
        Assert.Equal(Location.None, actual.Syntax.MultiplesName);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NamedProperties_Match(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42, GenerateMultiplesProperty = true, MultiplesName = "Constants")]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedValueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedGenerateMultiplesPropertyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);
        var expectedMultiplesNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 4);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal(42, actual.Value);
        Assert.True(actual.GenerateMultiplesProperty);
        Assert.Equal("Constants", actual.MultiplesName);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueLocation, actual.Syntax.Value);
        Assert.Equal(expectedGenerateMultiplesPropertyLocation, actual.Syntax.GenerateMultiplesProperty);
        Assert.Equal(expectedMultiplesNameLocation, actual.Syntax.MultiplesName);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task MultiplesName_ExplicitlyNull_Match(IConstructiveSyntacticAttributeParser<IRawScalarConstant> parser)
    {
        var source = """
            [SharpMeasures.ScalarConstant("Constant", "Metre", 42, MultiplesName = null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedUnitInstanceNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedValueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedMultiplesNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Constant", actual!.Name);
        Assert.Equal("Metre", actual.UnitInstanceName);
        Assert.Equal(42, actual.Value);
        Assert.Null(actual.GenerateMultiplesProperty);
        Assert.Null(actual.MultiplesName);

        Assert.Equal(expectedNameLocation, actual.Syntax!.Name);
        Assert.Equal(expectedUnitInstanceNameLocation, actual.Syntax.UnitInstanceName);
        Assert.Equal(expectedValueLocation, actual.Syntax.Value);
        Assert.Equal(Location.None, actual.Syntax.GenerateMultiplesProperty);
        Assert.Equal(expectedMultiplesNameLocation, actual.Syntax.MultiplesName);
    }
}
