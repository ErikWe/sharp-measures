namespace SharpMeasures.Generators.Parsing.Tests.QuantitiesCases.QuantityOperationAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Quantities;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawQuantityOperation? Target(IConstructiveSemanticAttributeParser<IRawQuantityOperation> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawQuantityOperation> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperatorType.Addition)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var actual = Target(parser, attributeData);

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
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSemanticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperatorType.Addition, MethodName = null, StaticMethodName = null, MirroredMethodName = null, MirroredStaticMethodName = null)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var actual = Target(parser, attributeData);

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
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Values_Match(IConstructiveSemanticAttributeParser<IRawQuantityOperation> parser)
    {
        var source = """
            [SharpMeasures.QuantityOperation<int, string>(SharpMeasures.OperatorType.Subtraction, Position = SharpMeasures.OperationPosition.Left, MirrorMode = SharpMeasures.OperationMirrorMode.Enable,
                Implementation = SharpMeasures.OperationImplementation.InstanceMethod, MirroredImplementation = SharpMeasures.OperationImplementation.StaticMethod, MethodName = "Method",
                StaticMethodName = "StaticMethod", MirroredMethodName = "MirroredMethod", MirroredStaticMethodName = "MirroredStaticMethod")]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);

        var actual = Target(parser, attributeData);

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
        Assert.Null(actual.Syntax);
    }
}
