namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.SpecializedVectorGroupAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawSpecializedVectorGroup? Target(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperations_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(InheritOperations = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.True(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversions_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(InheritConversions = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.True(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ForwardsCastOperatorBehaviour_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(ForwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BackwardsCastOperatorBehaviour_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(BackwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);
        Assert.Null(actual.Syntax);
    }
}
