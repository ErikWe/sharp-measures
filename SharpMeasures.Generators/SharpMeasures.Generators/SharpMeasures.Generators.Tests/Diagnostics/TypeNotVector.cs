namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotVector
{
    [Fact]
    public Task VerifyTypeNotVectorDiagnosticsMessage_Null()
    {
        var source = ScalarVectorText("null");
        var expectedLocation = ScalarVectorLocation("null");

        return AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotVectorDiagnosticsMessage_Int()
    {
        var source = ScalarVectorText("typeof(int)");
        var expectedLocation = ScalarVectorLocation("typeof(int)");

        return AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void ScalarVector_ExactList(string value)
    {
        var source = ScalarVectorText(value);
        var expectedLocation = ScalarVectorLocation(value);

        AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void SpecializedVectorOriginalVector_ExactList(string value)
    {
        var source = SpecializedVectorOriginalVectorText(value);
        var expectedLocation = SpecializedVectorOriginalVectorLocation(value);

        AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorDifference_ExactList(string value)
    {
        var source = VectorDifferenceText(value);
        var expectedLocation = VectorDifferenceLocation(value);

        AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void SpecializedVectorDifference_ExactList(string value)
    {
        var source = SpecializedVectorDifferenceText(value);
        var expectedLocation = SpecializedVectorDifferenceLocation(value);

        AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void ConvertibleQuantity_ExactList(string value)
    {
        if (value is "null")
        {
            value = "(System.Type)null";
        }

        var source = ConvertibleQuantityText(value);
        var expectedLocation = ConvertibleQuantityLocation(value);

        AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    private static IEnumerable<object[]> NonVectorTypes() => new object[][]
    {
        new[] { "null" },
        new[] { "typeof(int)" },
        new[] { "typeof(UnitOfLength)" },
        new[] { "typeof(Length)" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotVectorDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> TypeNotVectorDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVector };

    private static string ScalarVectorText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), Vector = {{value}})]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> ScalarVectorLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(ScalarVectorText(value), value, prefix: "Vector = ");

    private static string SpecializedVectorOriginalVectorText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector({{value}})]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedVectorOriginalVectorLocation(string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(SpecializedVectorOriginalVectorText(value), value, prefix: "SpecializedSharpMeasuresVector(");

    private static string VectorDifferenceText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Difference = {{value}})]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> VectorDifferenceLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(VectorDifferenceText(value), value, prefix: "Difference = ");

    private static string SpecializedVectorDifferenceText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), Difference = {{value}})]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedVectorDifferenceLocation(string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(SpecializedVectorDifferenceText(value), value, prefix: "Difference = ");

    private static string ConvertibleQuantityText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity({{value}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> ConvertibleQuantityLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(ConvertibleQuantityText(value), value, prefix: "ConvertibleQuantity(");
}
