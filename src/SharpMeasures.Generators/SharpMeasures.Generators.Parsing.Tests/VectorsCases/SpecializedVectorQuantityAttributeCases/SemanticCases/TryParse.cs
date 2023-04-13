namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.SpecializedVectorQuantityAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawSpecializedVectorQuantity? Target(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperations_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritOperations = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.True(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProcesses_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritProcesses = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.True(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProperties_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritProperties = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.True(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConstants_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritConstants = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.True(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversions_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritConversions = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.True(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ForwardsCastOperatorBehaviour_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(ForwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BackwardsCastOperatorBehaviour_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(BackwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConstants);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }
}
