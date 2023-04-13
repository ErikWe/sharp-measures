namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.SpecializedVectorQuantityAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawSpecializedVectorQuantity? Target(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperations_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritOperations = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritOperationsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(expectedInheritOperationsLocation, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProcesses_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritProcesses = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritProcessesLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(expectedInheritProcessesLocation, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProperties_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritProperties = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritPropertiesLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(expectedInheritPropertiesLocation, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConstants_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritConstants = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConstantsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(expectedInheritConstantsLocation, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversions_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(InheritConversions = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConversionsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(expectedInheritConversionsLocation, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ForwardsCastOperatorBehaviour_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(ForwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedForwardsCastOperatorBehaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(expectedForwardsCastOperatorBehaviourLocation, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task BackwardsCastOperatorBehaviour_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(BackwardsCastOperatorBehaviour = SharpMeasures.ConversionOperatorBehaviour.Implicit)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedBackwardsCastOperatorBehaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(expectedBackwardsCastOperatorBehaviourLocation, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementSumLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(expectedImplementSumLocation, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedVectorQuantity<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedScalarLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementDifferenceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

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

        Assert.Equal(expectedScalarLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConstants);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ForwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.BackwardsCastOperatorBehaviour);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(expectedImplementDifferenceLocation, actual.Syntax.ImplementDifference);
    }
}
