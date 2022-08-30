namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class EmptyList
{
    [Fact]
    public Task VerifyEmptyListDiagnosticsMessage_Quantity()
    {
        var attribute = ParseAttributeAndArguments("ConvertibleQuantity", EmptyParamsList);

        return AssertScalarAttribute(attribute).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyEmptyListDiagnosticsMessage_Unit()
    {
        var attribute = ParseAttributeAndArguments("IncludeBases", EmptyParamsList);

        return AssertScalarAttribute(attribute).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(ScalarAttributesAndArguments))]
    public void Scalar(SourceSubtext attribute) => AssertScalarAttribute(attribute);

    [Theory]
    [MemberData(nameof(ScalarAttributesAndArguments))]
    public void SpecializedScalar(SourceSubtext attribute) => AssertSpecializedScalarAttribute(attribute);

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void Vector(SourceSubtext attribute) => AssertVectorAttribute(attribute);

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void SpecializedVector(SourceSubtext attribute) => AssertSpecializedVectorAttribute(attribute);

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void VectorGroup(SourceSubtext attribute) => AssertVectorGroupAttribute(attribute);

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void SpecializedVectorGroup(SourceSubtext attribute) => AssertSpecializedVectorGroupAttribute(attribute);

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void VectorGroupMember(SourceSubtext attribute) => AssertVectorGroupMemberAttribute(attribute);

    private static IEnumerable<object[]> Arguments(string type) => new object[][]
    {
        new object[] { EmptyParamsList },
        new object[] { ExplicitlyEmptyList(type) },
        new object[] { ImplicitlyEmptyList(type) }
    };

    private static (SourceSubtext Text, bool ShouldTargetAttribute) EmptyParamsList => (new(string.Empty, SourceLocationContext.Empty), true);
    private static (SourceSubtext Text, bool ShouldTargetAttribute) ExplicitlyEmptyList(string type) => (SourceSubtext.Covered($"{type}[0]", prefix: "new "), false);
    private static (SourceSubtext Text, bool ShouldTargetAttribute) ImplicitlyEmptyList(string type) => (SourceSubtext.Covered("{ }", prefix: $"new {type}[] "), false);

    public static IEnumerable<object[]> QuantityAttributesAndArguments()
    {
        IEnumerable<(string AttributeName, string Type)> attributes = new[]
        {
            ("ConvertibleQuantity", "System.Type"),
            ("IncludeUnits", "string"),
            ("ExcludeUnits", "string")
        };

        foreach ((var attributeName, var type) in attributes)
        {
            foreach (var argumentObject in Arguments(type))
            {
                yield return new object[] { ParseAttributeAndArguments(attributeName, ((SourceSubtext, bool))argumentObject[0]) };
            }
        }
    }

    public static IEnumerable<object[]> ScalarAttributesAndArguments()
    {
        IEnumerable<(string AttributeName, string Type)> additionalAttributes = new[]
        {
            ("IncludeBases", "string"),
            ("ExcludeBases", "string")
        };

        foreach (var originalList in QuantityAttributesAndArguments())
        {
            yield return originalList;
        }

        foreach ((var attributeName, var type) in additionalAttributes)
        {
            foreach (var argumentObject in Arguments(type))
            {
                yield return new object[] { ParseAttributeAndArguments(attributeName, ((SourceSubtext, bool))argumentObject[0]) };
            }
        }
    }

    private static SourceSubtext ParseAttributeAndArguments(string attributeName, (SourceSubtext Text, bool ShouldTargetAttribute) argument)
    {
        if (argument.ShouldTargetAttribute)
        {
            return SourceSubtext.Covered($"{attributeName}()", prefix: argument.Text.Context.Prefix, postfix: argument.Text.Context.Postfix);
        }

        return SourceSubtext.Covered(argument.Text.Context.Target, prefix: $"{attributeName}({argument.Text.Context.Prefix}", postfix: $"{argument.Text.Context.Postfix})");
    }

    private static GeneratorVerifier AssertExactlyEmptyListDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(EmptyListDiagnostics);
    private static IReadOnlyCollection<string> EmptyListDiagnostics { get; } = new string[] { DiagnosticIDs.EmptyList };

    private static string ScalarAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarAttribute(SourceSubtext attribute)
    {
        var source = ScalarAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarAttributeIdentical);
    }

    private static string SpecializedScalarAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarAttribute(SourceSubtext attribute)
    {
        var source = SpecializedScalarAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarAttributeIdentical);
    }

    private static string VectorAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorAttribute(SourceSubtext attribute)
    {
        var source = VectorAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorAttributeIdentical);
    }

    private static string SpecializedVectorAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorAttribute(SourceSubtext attribute)
    {
        var source = SpecializedVectorAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorAttributeIdentical);
    }

    private static string VectorGroupAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupAttribute(SourceSubtext attribute)
    {
        var source = VectorGroupAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupAttributeIdentical);
    }

    private static string SpecializedVectorGroupAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
            
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroupAttribute(SourceSubtext attribute)
    {
        var source = SpecializedVectorGroupAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupAttributeIdentical);
    }

    private static string VectorGroupMemberAttributeText(SourceSubtext attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
            
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMemberAttribute(SourceSubtext attribute)
    {
        var source = VectorGroupMemberAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, attribute.Context);

        return AssertExactlyEmptyListDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberAttributeIdentical);
    }

    private static GeneratorVerifier ScalarAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarAttributeIdenticalText);
    private static GeneratorVerifier SpecializedScalarAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarAttributeIdenticalText);
    private static GeneratorVerifier VectorAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorAttributeIdenticalText);
    private static GeneratorVerifier SpecializedVectorAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorAttributeIdenticalText);
    private static GeneratorVerifier VectorGroupAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupAttributeIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupAttributeIdenticalText);
    private static GeneratorVerifier VectorGroupMemberAttributeIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberAttributeIdenticalText);

    private static string ScalarAttributeIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarAttributeIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorAttributeIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorAttributeIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupAttributeIdenticalText => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupAttributeIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
            
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberAttributeIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
            
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
