namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.SpecializedVectorGroupAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawSpecializedVectorGroup? Target(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperations_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(InheritOperations = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritOperationsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.True(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(expectedInheritOperationsLocation, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversions_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(InheritConversions = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConversionsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.True(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(expectedInheritConversionsLocation, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ForwardsCastOperatorBehaviour_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(ForwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedForwardsCastOperatorBehaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(expectedForwardsCastOperatorBehaviourLocation, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BackwardsCastOperatorBehaviour_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(BackwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedBackwardsCastOperatorBehaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Equal(ConversionOperatorBehaviour.Implicit, actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(expectedBackwardsCastOperatorBehaviourLocation, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementSumLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(expectedImplementSumLocation, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorGroup<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementDifferenceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ForwardsCastOperatorBehaviour);
        Assert.Null(actual.BackwardsCastOperatorBehaviour);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(expectedImplementDifferenceLocation, actual.Syntax.ImplementDifference);
    }
}
