namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyTypeNotVectorDiagnosticsMessage_Null() => AssertAndVerifyScalarVector(NullType);

    [Fact]
    public Task VerifyTypeNotVectorDiagnosticsMessage_Int() => AssertAndVerifyScalarVector(IntType);

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
        new object[] { NullType },
        new object[] { IntType },
        new object[] { UnitOfLengthType },
        new object[] { LengthType }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", prefix: "(System.Type)");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int");
    private static SourceSubtext UnitOfLengthType { get; } = SourceSubtext.AsTypeof("UnitOfLength");
    private static SourceSubtext LengthType { get; } = SourceSubtext.AsTypeof("Length");

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

    private static GeneratorVerifier AssertScalarVector(SourceSubtext vectorType)
    {
        var source = ScalarVectorText(vectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, vectorType.Context.With(outerPrefix: "Vector = "));

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

    private static GeneratorVerifier AssertSpecializedVectorOriginalVector(SourceSubtext originalVectorType)
    {
        var source = SpecializedVectorOriginalVectorText(originalVectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, originalVectorType.Context.With(outerPrefix: "SpecializedSharpMeasuresVector("));

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

    private static GeneratorVerifier AssertVectorDifference(SourceSubtext differenceVectorType)
    {
        var source = VectorDifferenceText(differenceVectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, differenceVectorType.Context.With(outerPrefix: "Difference = "));

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

    private static GeneratorVerifier AssertSpecializedVectorDifference(SourceSubtext differenceVectorType)
    {
        var source = SpecializedVectorDifferenceText(differenceVectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, differenceVectorType.Context.With(outerPrefix: "Difference = "));

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

    private static GeneratorVerifier AssertConvertibleQuantity(SourceSubtext quantityType)
    {
        var source = ConvertibleQuantityText(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "ConvertibleQuantity("));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
