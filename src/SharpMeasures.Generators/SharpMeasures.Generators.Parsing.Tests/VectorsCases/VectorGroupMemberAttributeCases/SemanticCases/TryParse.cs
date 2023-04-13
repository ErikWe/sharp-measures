namespace SharpMeasures.Generators.Parsing.Tests.VectorsCases.VectorGroupMemberAttributeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Vectors;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawVectorGroupMember? Target(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public void NullAttributeData_ArgumentNullException(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task DefaultValues_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Dimension_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(Dimension = 42)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Equal(42, actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperationsFromGroup_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritOperationsFromGroup = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.True(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperationsFromMembers_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritOperationsFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.True(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProcessesFromMembers_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritProcessesFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.True(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritPropertiesFromMembers_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritPropertiesFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.True(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConstantsFromMembers_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritConstantsFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.True(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversionsFromGroup_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritConversionsFromGroup = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.True(actual.InheritConversionsFromGroup);
        Assert.Null(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversionsFromMembers_Match(IConstructiveSemanticAttributeParser<IRawVectorGroupMember> parser)
    {
        var source = """
            [SharpMeasures.VectorGroupMember<int>(InheritConversionsFromMembers = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var actual = Target(parser, attributeData);

        Assert.Equal(intType, actual!.Group);
        Assert.Null(actual.Dimension);
        Assert.Null(actual.InheritOperationsFromGroup);
        Assert.Null(actual.InheritOperationsFromMembers);
        Assert.Null(actual.InheritProcessesFromMembers);
        Assert.Null(actual.InheritPropertiesFromMembers);
        Assert.Null(actual.InheritConstantsFromMembers);
        Assert.Null(actual.InheritConversionsFromGroup);
        Assert.True(actual.InheritConversionsFromMembers);
        Assert.Null(actual.Syntax);
    }
}
