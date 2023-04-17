namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.QuantityOperationAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawQuantityOperation? Target(IConstructiveSyntacticAttributeParser<IRawQuantityOperation> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperationType.Addition)]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperationType.Addition)]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperatorType.Addition)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedOtherLocation = ExpectedLocation.TypeArgument(attributeSyntax, 1);
        var expectedOperatorTypeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal(stringType, actual.Other);
        Assert.Equal(OperatorType.Addition, actual.OperatorType);
        Assert.Null(actual.Position);
        Assert.Null(actual.MirrorMode);
        Assert.Null(actual.Implementation);
        Assert.Null(actual.MirroredImplementation);
        Assert.Null(actual.MethodName);
        Assert.Null(actual.StaticMethodName);
        Assert.Null(actual.MirroredMethodName);
        Assert.Null(actual.MirroredStaticMethodName);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedResultLocation, actual.Syntax.Result);
        Assert.Equal(expectedOtherLocation, actual.Syntax.Other);
        Assert.Equal(expectedOperatorTypeLocation, actual.Syntax.OperatorType);
        Assert.Equal(Location.None, actual.Syntax.Position);
        Assert.Equal(Location.None, actual.Syntax.MirrorMode);
        Assert.Equal(Location.None, actual.Syntax.Implementation);
        Assert.Equal(Location.None, actual.Syntax.MirroredImplementation);
        Assert.Equal(Location.None, actual.Syntax.MethodName);
        Assert.Equal(Location.None, actual.Syntax.StaticMethodName);
        Assert.Equal(Location.None, actual.Syntax.MirroredMethodName);
        Assert.Equal(Location.None, actual.Syntax.MirroredStaticMethodName);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperatorType.Addition, MethodName = null, StaticMethodName = null, MirroredMethodName = null, MirroredStaticMethodName = null)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedOtherLocation = ExpectedLocation.TypeArgument(attributeSyntax, 1);
        var expectedOperatorTypeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedStaticMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedMirroredMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);
        var expectedMirroredStaticMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 4);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal(stringType, actual.Other);
        Assert.Equal(OperatorType.Addition, actual.OperatorType);
        Assert.Null(actual.Position);
        Assert.Null(actual.MirrorMode);
        Assert.Null(actual.Implementation);
        Assert.Null(actual.MirroredImplementation);
        Assert.Null(actual.MethodName);
        Assert.Null(actual.StaticMethodName);
        Assert.Null(actual.MirroredMethodName);
        Assert.Null(actual.MirroredStaticMethodName);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedResultLocation, actual.Syntax.Result);
        Assert.Equal(expectedOtherLocation, actual.Syntax.Other);
        Assert.Equal(expectedOperatorTypeLocation, actual.Syntax.OperatorType);
        Assert.Equal(Location.None, actual.Syntax.Position);
        Assert.Equal(Location.None, actual.Syntax.MirrorMode);
        Assert.Equal(Location.None, actual.Syntax.Implementation);
        Assert.Equal(Location.None, actual.Syntax.MirroredImplementation);
        Assert.Equal(expectedMethodNameLocation, actual.Syntax.MethodName);
        Assert.Equal(expectedStaticMethodNameLocation, actual.Syntax.StaticMethodName);
        Assert.Equal(expectedMirroredMethodNameLocation, actual.Syntax.MirroredMethodName);
        Assert.Equal(expectedMirroredStaticMethodNameLocation, actual.Syntax.MirroredStaticMethodName);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSyntacticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperatorType.Subtraction, Position = SharpMeasures.OperationPosition.Left, MirrorMode = SharpMeasures.OperationMirrorMode.Enable,
                Implementation = SharpMeasures.OperationImplementation.InstanceMethod, MirroredImplementation = SharpMeasures.OperationImplementation.StaticMethod, MethodName = "Method",
                StaticMethodName = "StaticMethod", MirroredMethodName = "MirroredMethod", MirroredStaticMethodName = "MirroredStaticMethod")]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedResultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedOtherLocation = ExpectedLocation.TypeArgument(attributeSyntax, 1);
        var expectedOperatorTypeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPositionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedMirrorModeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var expectedImplementationLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);
        var expectedMirroredImplementationLocation = ExpectedLocation.SingleArgument(attributeSyntax, 4);
        var expectedMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 5);
        var expectedStaticMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 6);
        var expectedMirroredMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 7);
        var expectedMirroredStaticMethodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 8);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Result);
        Assert.Equal(stringType, actual.Other);
        Assert.Equal(OperatorType.Subtraction, actual.OperatorType);
        Assert.Equal(OperationPosition.Left, actual.Position);
        Assert.Equal(OperationMirrorMode.Enable, actual.MirrorMode);
        Assert.Equal(OperationImplementation.InstanceMethod, actual.Implementation);
        Assert.Equal(OperationImplementation.StaticMethod, actual.MirroredImplementation);
        Assert.Equal("Method", actual.MethodName);
        Assert.Equal("StaticMethod", actual.StaticMethodName);
        Assert.Equal("MirroredMethod", actual.MirroredMethodName);
        Assert.Equal("MirroredStaticMethod", actual.MirroredStaticMethodName);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedResultLocation, actual.Syntax.Result);
        Assert.Equal(expectedOtherLocation, actual.Syntax.Other);
        Assert.Equal(expectedOperatorTypeLocation, actual.Syntax.OperatorType);
        Assert.Equal(expectedPositionLocation, actual.Syntax.Position);
        Assert.Equal(expectedMirrorModeLocation, actual.Syntax.MirrorMode);
        Assert.Equal(expectedImplementationLocation, actual.Syntax.Implementation);
        Assert.Equal(expectedMirroredImplementationLocation, actual.Syntax.MirroredImplementation);
        Assert.Equal(expectedMethodNameLocation, actual.Syntax.MethodName);
        Assert.Equal(expectedStaticMethodNameLocation, actual.Syntax.StaticMethodName);
        Assert.Equal(expectedMirroredMethodNameLocation, actual.Syntax.MirroredMethodName);
        Assert.Equal(expectedMirroredStaticMethodNameLocation, actual.Syntax.MirroredStaticMethodName);
    }
}
