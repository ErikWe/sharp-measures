namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.SpecializedScalarQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawSpecializedScalarQuantity? Target(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AllowNegative_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(AllowNegative = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.True(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperations_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(InheritOperations = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.True(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProcesses_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(InheritProcesses = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.True(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProperties_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(InheritProperties = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.True(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConstants_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(InheritConstants = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.True(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversions_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(InheritConversions = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.True(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedScalarQuantity<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }
}
