﻿namespace SharpMeasures.Generators.Parsing.Tests.ScalarsCases.SpecializedUnitlessQuantityAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Scalars;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawSpecializedUnitlessQuantity? Target(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task Defaults_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task AllowNegative_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(AllowNegative = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedAllowNegativeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.True(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(expectedAllowNegativeLocation, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritOperations_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(InheritOperations = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritOperationsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.True(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(expectedInheritOperationsLocation, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProcesses_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(InheritProcesses = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritProcessesLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.True(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(expectedInheritProcessesLocation, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritProperties_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(InheritProperties = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritPropertiesLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.True(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(expectedInheritPropertiesLocation, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task InheritConversions_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(InheritConversions = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedInheritConversionsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.True(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(expectedInheritConversionsLocation, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementSum_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(ImplementSum = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementSumLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.True(actual.ImplementSum);
        Assert.Null(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(expectedImplementSumLocation, actual.Syntax.ImplementSum);
        Assert.Equal(Location.None, actual.Syntax.ImplementDifference);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task ImplementDifference_Match(IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity> parser)
    {
        var source = """
            [SharpMeasures.SpecializedUnitlessQuantity<int>(ImplementDifference = true)]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var intType = compilation.GetSpecialType(SpecialType.System_Int32);

        var expectedOriginalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var expectedImplementDifferenceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal(intType, actual!.Original);
        Assert.Null(actual.AllowNegative);
        Assert.Null(actual.InheritOperations);
        Assert.Null(actual.InheritProcesses);
        Assert.Null(actual.InheritProperties);
        Assert.Null(actual.InheritConversions);
        Assert.Null(actual.ImplementSum);
        Assert.True(actual.ImplementDifference);

        Assert.Equal(expectedOriginalLocation, actual.Syntax!.Original);
        Assert.Equal(Location.None, actual.Syntax.AllowNegative);
        Assert.Equal(Location.None, actual.Syntax.InheritOperations);
        Assert.Equal(Location.None, actual.Syntax.InheritProcesses);
        Assert.Equal(Location.None, actual.Syntax.InheritProperties);
        Assert.Equal(Location.None, actual.Syntax.InheritConversions);
        Assert.Equal(Location.None, actual.Syntax.ImplementSum);
        Assert.Equal(expectedImplementDifferenceLocation, actual.Syntax.ImplementDifference);
    }
}