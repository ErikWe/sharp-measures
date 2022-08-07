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
    public Task VerifyTypeNotVectorDiagnosticsMessage_Null() => AssertAndVerifyScalarVector(NullSubtext);

    [Fact]
    public Task VerifyTypeNotVectorDiagnosticsMessage_Int() => AssertAndVerifyScalarVector(IntSubtext);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void ScalarVector(SourceSubtext vectorType) => AssertScalarVector(vectorType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void SpecializedVectorOriginalVector(SourceSubtext originalVectorType) => AssertSpecializedVectorOriginalVector(originalVectorType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorDifference(SourceSubtext differenceVectorType) => AssertVectorDifference(differenceVectorType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void SpecializedVectorDifference(SourceSubtext differenceVectorType) => AssertSpecializedVectorDifference(differenceVectorType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void ConvertibleQuantity(SourceSubtext quantityType) => AssertConvertibleQuantity(quantityType);

    private static IEnumerable<object[]> NonVectorTypes() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { IntSubtext },
        new object[] { UnitOfLengthSubtext },
        new object[] { LengthSubtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null");
    private static SourceSubtext IntSubtext { get; } = SourceSubtext.Typeof("int");
    private static SourceSubtext UnitOfLengthSubtext { get; } = SourceSubtext.Typeof("UnitOfLength");
    private static SourceSubtext LengthSubtext { get; } = SourceSubtext.Typeof("Length");

    private static GeneratorVerifier AssertExactlyTypeNotVectorDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorDiagnostics);
    private static IReadOnlyCollection<string> TypeNotVectorDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVector };

    private static string ScalarVectorText(SourceSubtext vectorType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), Vector = {{vectorType}})]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ScalarVectorLocation(SourceSubtext vectorType) => ExpectedDiagnosticsLocation.TextSpan(ScalarVectorText(vectorType), vectorType, prefix: "Vector = ");

    private static GeneratorVerifier AssertScalarVector(SourceSubtext vectorType)
    {
        var source = ScalarVectorText(vectorType);
        var expectedLocation = ScalarVectorLocation(vectorType);

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyScalarVector(SourceSubtext vectorType) => AssertScalarVector(vectorType).VerifyDiagnostics();

    private static string SpecializedVectorOriginalVectorText(SourceSubtext originalVectorType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector({{originalVectorType}})]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedVectorOriginalVectorLocation(SourceSubtext originalVectorType) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorOriginalVectorText(originalVectorType), originalVectorType, prefix: "SpecializedSharpMeasuresVector(");

    private static GeneratorVerifier AssertSpecializedVectorOriginalVector(SourceSubtext originalVectorType)
    {
        var source = SpecializedVectorOriginalVectorText(originalVectorType);
        var expectedLocation = SpecializedVectorOriginalVectorLocation(originalVectorType);

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorDifferenceText(SourceSubtext differenceVectorType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Difference = {{differenceVectorType}})]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorDifferenceLocation(SourceSubtext differenceVectorType) => ExpectedDiagnosticsLocation.TextSpan(VectorDifferenceText(differenceVectorType), differenceVectorType, prefix: "Difference = ");

    private static GeneratorVerifier AssertVectorDifference(SourceSubtext differenceVectorType)
    {
        var source = VectorDifferenceText(differenceVectorType);
        var expectedLocation = VectorDifferenceLocation(differenceVectorType);

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorDifferenceText(SourceSubtext differenceVectorType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), Difference = {{differenceVectorType}})]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedVectorDifferenceLocation(SourceSubtext differenceVectorType) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorDifferenceText(differenceVectorType), differenceVectorType, prefix: "Difference = ");

    private static GeneratorVerifier AssertSpecializedVectorDifference(SourceSubtext differenceVectorType)
    {
        var source = SpecializedVectorDifferenceText(differenceVectorType);
        var expectedLocation = SpecializedVectorDifferenceLocation(differenceVectorType);

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string ConvertibleQuantityText(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity({{quantityType}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleQuantityLocation(SourceSubtext quantityType) => ExpectedDiagnosticsLocation.TextSpan(ConvertibleQuantityText(quantityType), quantityType, prefix: "ConvertibleQuantity(");

    private static GeneratorVerifier AssertConvertibleQuantity(SourceSubtext quantityType)
    {
        if (quantityType.Target is "null")
        {
            quantityType = quantityType with { Prefix = $"{quantityType.Prefix}(System.Type)" };
        }

        var source = ConvertibleQuantityText(quantityType);
        var expectedLocation = ConvertibleQuantityLocation(quantityType);

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
