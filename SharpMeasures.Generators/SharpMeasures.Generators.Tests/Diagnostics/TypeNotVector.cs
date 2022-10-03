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
    public Task VerifyTypeNotVectorDiagnosticsMessage_Null() => AssertScalarVector(NullType).VerifyDiagnostics();

    [Fact]
    public Task VerifyTypeNotVectorDiagnosticsMessage_Int() => AssertScalarVector(IntType).VerifyDiagnostics();

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

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorOperation_Vector(SourceSubtext quantityType) => AssertVectorOperation_Vector(quantityType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorOperation_SpecializedVector(SourceSubtext quantityType) => AssertVectorOperation_SpecializedVector(quantityType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorOperation_VectorGroup(SourceSubtext quantityType) => AssertVectorOperation_VectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorOperation_SpecializedVectorGroup(SourceSubtext quantityType) => AssertVectorOperation_SpecializedVectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorOperation_VectorGroupMember(SourceSubtext quantityType) => AssertVectorOperation_VectorGroupMember(quantityType);

    public static IEnumerable<object[]> NonVectorTypes() => new object[][]
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
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength), Vector = {{vectorType}})]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarVector(SourceSubtext vectorType)
    {
        var source = ScalarVectorText(vectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, vectorType.Context.With(outerPrefix: "Vector = "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarVectorIdentical);
    }

    private static string SpecializedVectorOriginalVectorText(SourceSubtext originalVectorType) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity({{originalVectorType}})]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorOriginalVector(SourceSubtext originalVectorType)
    {
        var source = SpecializedVectorOriginalVectorText(originalVectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, originalVectorType.Context.With(outerPrefix: "SpecializedVectorQuantity("));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorOriginalVectorIdentical);
    }

    private static string VectorDifferenceText(SourceSubtext differenceVectorType) => $$"""
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength), Difference = {{differenceVectorType}})]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDifference(SourceSubtext differenceVectorType)
    {
        var source = VectorDifferenceText(differenceVectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, differenceVectorType.Context.With(outerPrefix: "Difference = "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorDifferenceIdentical);
    }

    private static string SpecializedVectorDifferenceText(SourceSubtext differenceVectorType) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), Difference = {{differenceVectorType}})]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDifference(SourceSubtext differenceVectorType)
    {
        var source = SpecializedVectorDifferenceText(differenceVectorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, differenceVectorType.Context.With(outerPrefix: "Difference = "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorDifferenceIdentical);
    }

    private static string ConvertibleQuantityText(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity({{quantityType}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleQuantity(SourceSubtext quantityType)
    {
        var source = ConvertibleQuantityText(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "ConvertibleQuantity("));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleQuantityIdentical);
    }

    private static string VectorOperationText_Vector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [VectorOperation(typeof(Length), {{quantityType}}, VectorOperatorType.Dot)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_Vector(SourceSubtext quantityType)
    {
        var source = VectorOperationText_Vector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation(typeof(Length), "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string VectorOperationText_SpecializedVector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), {{quantityType}}, VectorOperatorType.Dot)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_SpecializedVector(SourceSubtext quantityType)
    {
        var source = VectorOperationText_SpecializedVector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation(typeof(Length), "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorOperationText_VectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), {{quantityType}}, VectorOperatorType.Dot)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroup(SourceSubtext quantityType)
    {
        var source = VectorOperationText_VectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation(typeof(Length), "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string VectorOperationText_SpecializedVectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), {{quantityType}}, VectorOperatorType.Dot)]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_SpecializedVectorGroup(SourceSubtext quantityType)
    {
        var source = VectorOperationText_SpecializedVectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation(typeof(Length), "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorOperationText_VectorGroupMember(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), {{quantityType}}, VectorOperatorType.Dot)]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroupMember(SourceSubtext quantityType)
    {
        var source = VectorOperationText_VectorGroupMember(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation(typeof(Length), "));

        return AssertExactlyTypeNotVectorDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarVectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorOriginalVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorOriginalVectorIdenticalText);
    private static GeneratorVerifier VectorDifferenceIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorDifferenceIdenticalText);
    private static GeneratorVerifier SpecializedVectorDifferenceIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorDifferenceIdenticalText);
    private static GeneratorVerifier ConvertibleQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleQuantityIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarVectorIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorOriginalVectorIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorDifferenceIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorDifferenceIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), ImplementDifference = false)]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleQuantityIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
