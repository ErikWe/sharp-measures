namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorGroupMemberAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorGroupMember? Target(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Dimension_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(Dimension = 42)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedDimensionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Equal(42, actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(expectedDimensionLocation, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperationsFromGroup_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritOperationsFromGroup = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritOperationsFromGroupLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.True(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(expectedInheritOperationsFromGroupLocation, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperationsFromMembers_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritOperationsFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritOperationsFromMembersLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.True(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(expectedInheritOperationsFromMembersLocation, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProcessesFromMembers_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritProcessesFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritProcessesFromMembersLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.True(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(expectedInheritProcessesFromMembersLocation, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritPropertiesFromMembers_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritPropertiesFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritPropertiesFromMembersLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.True(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(expectedInheritPropertiesFromMembersLocation, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConstantsFromMembers_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritConstantsFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConstantsFromMembersLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.True(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(expectedInheritConstantsFromMembersLocation, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversionsFromGroup_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritConversionsFromGroup = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConversionsFromGroupLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.True(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(expectedInheritConversionsFromGroupLocation, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromMembers);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversionsFromMembers_Match(IConstructiveSyntacticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritConversionsFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedGroupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConversionsFromMembersLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.True(actual.InheritConversionsFromMembers);

        Assert.Equal(expectedGroupLocation, actual.Syntax!.Group);
        Assert.Equal(Location.None, actual.Syntax.Dimension);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromGroup);
        Assert.Equal(Location.None, actual.Syntax.InheritOperationsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritProcessesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritPropertiesFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConstantsFromMembers);
        Assert.Equal(Location.None, actual.Syntax.InheritConversionsFromGroup);
        Assert.Equal(expectedInheritConversionsFromMembersLocation, actual.Syntax.InheritConversionsFromMembers);
    }
}
