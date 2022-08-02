namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
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
        string source = ScalarAttributeText("ConvertibleQuantity", string.Empty);

        return AssertExactlyEmptyListDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyEmptyListDiagnosticsMessage_Unit()
    {
        string source = ScalarAttributeText("IncludeBases", string.Empty);

        return AssertExactlyEmptyListDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(ScalarAttributesAndArguments))]
    public void Scalar_ExactList(string attribute, string value)
    {
        string source = ScalarAttributeText(attribute, value);

        AssertExactlyEmptyListDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(ScalarAttributesAndArguments))]
    public void ConvertibleSpecializedScalar_ExactList(string attribute, string value)
    {
        string source = SpecializedScalarAttributeText(attribute, value);

        AssertExactlyEmptyListDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void ConvertibleVector_ExactList(string attribute, string value)
    {
        string source = VectorAttributeText(attribute, value);

        AssertExactlyEmptyListDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void ConvertibleSpecializedVector_ExactList(string attribute, string value)
    {
        string source = SpecializedVectorAttributeText(attribute, value);

        AssertExactlyEmptyListDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void ConvertibleVectorGroup_ExactList(string attribute, string value)
    {
        string source = VectorGroupAttributeText(attribute, value);

        AssertExactlyEmptyListDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(QuantityAttributesAndArguments))]
    public void ConvertibleSpecializedVectorGroup_ExactList(string attribute, string value)
    {
        string source = SpecializedVectorGroupAttributeText(attribute, value);

        AssertExactlyEmptyListDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> Arguments(string type) => new object[][]
    {
        new[] { string.Empty },
        new[] { $$"""new {{type}}[] { }""" }
    };

    private static IEnumerable<object[]> QuantityAttributesAndArguments()
    {
        IEnumerable<(string, string)> attributes = new[] { ("ConvertibleQuantity", "System.Type"), ("IncludeUnits", "string"), ("ExcludeUnits", "string") };

        foreach (var attribute in attributes)
        {
            foreach (var argument in Arguments(attribute.Item2))
            {
                yield return new string[] { attribute.Item1, (string)argument[0] };
            }
        }
    }

    private static IEnumerable<object[]> ScalarAttributesAndArguments()
    {
        IEnumerable<(string, string)> additionalAttributes = new[] { ("IncludeBases", "string"), ("ExcludeBases", "string") };

        foreach (var originalList in QuantityAttributesAndArguments())
        {
            yield return originalList;
        }

        foreach (var attribute in additionalAttributes)
        {
            foreach (var argument in Arguments(attribute.Item2))
            {
                yield return new string[] { attribute.Item1, (string)argument[0] };
            }
        }
    }

    private static GeneratorVerifier AssertExactlyEmptyListDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(EmptyListDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> EmptyListDiagnostics { get; } = new string[] { DiagnosticIDs.EmptyList };

    private static string ScalarAttributeText(string attribute, string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}({{value}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarAttributeText(string attribute, string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}({{value}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorAttributeText(string attribute, string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{value}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorAttributeText(string attribute, string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{value}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupAttributeText(string attribute, string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{value}})]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupAttributeText(string attribute, string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{value}})]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
            
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
