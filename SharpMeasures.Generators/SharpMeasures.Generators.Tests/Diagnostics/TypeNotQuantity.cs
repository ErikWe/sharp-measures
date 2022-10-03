namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotQuantity
{
    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Null() => AssertQuantityOperationResult_Scalar(NullType).VerifyDiagnostics();

    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Int() => AssertQuantityOperationResult_Scalar(IntType).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_Scalar(SourceSubtext quantityType) => AssertQuantityOperationResult_Scalar(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_Scalar(SourceSubtext quantityType) => AssertQuantityOperationOther_Scalar(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_SpecializedScalar(SourceSubtext quantityType) => AssertQuantityOperationResult_SpecializedScalar(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_SpecializedScalar(SourceSubtext quantityType) => AssertQuantityOperationOther_SpecializedScalar(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_Vector(SourceSubtext quantityType) => AssertQuantityOperationResult_Vector(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_Vector(SourceSubtext quantityType) => AssertQuantityOperationOther_Vector(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_SpecializedVector(SourceSubtext quantityType) => AssertQuantityOperationResult_SpecializedVector(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_SpecializedVector(SourceSubtext quantityType) => AssertQuantityOperationOther_SpecializedVector(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_VectorGroup(SourceSubtext quantityType) => AssertQuantityOperationResult_VectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_VectorGroup(SourceSubtext quantityType) => AssertQuantityOperationOther_VectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_SpecializedVectorGroup(SourceSubtext quantityType) => AssertQuantityOperationResult_SpecializedVectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_SpecializedVectorGroup(SourceSubtext quantityType) => AssertQuantityOperationOther_SpecializedVectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationResult_VectorGroupMember(SourceSubtext quantityType) => AssertQuantityOperationResult_VectorGroupMember(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void QuantityOperationOther_VectorGroupMember(SourceSubtext quantityType) => AssertQuantityOperationOther_VectorGroupMember(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void VectorOperation_Vector(SourceSubtext quantityType) => AssertVectorOperation_Vector(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void VectorOperation_SpecializedVector(SourceSubtext quantityType) => AssertVectorOperation_SpecializedVector(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void VectorOperation_VectorGroup(SourceSubtext quantityType) => AssertVectorOperation_VectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void VectorOperation_SpecializedVectorGroup(SourceSubtext quantityType) => AssertVectorOperation_SpecializedVectorGroup(quantityType);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void VectorOperation_VectorGroupMember(SourceSubtext quantityType) => AssertVectorOperation_VectorGroupMember(quantityType);

    public static IEnumerable<object[]> NonQuantityTypes() => new object[][]
    {
        new object[] { NullType },
        new object[] { IntType },
        new object[] { UnitOfLengthType }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", prefix: "(System.Type)");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int");
    private static SourceSubtext UnitOfLengthType { get; } = SourceSubtext.AsTypeof("UnitOfLength");

    private static GeneratorVerifier AssertExactlyTypeNotQuantityDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotQuantityDiagnostics);
    private static IReadOnlyCollection<string> TypeNotQuantityDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotQuantity };

    private static string QuantityOperationResultText_Scalar(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_Scalar(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_Scalar(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string QuantityOperationOtherText_Scalar(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_Scalar(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_Scalar(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string QuantityOperationResultText_SpecializedScalar(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_SpecializedScalar(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_SpecializedScalar(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string QuantityOperationOtherText_SpecializedScalar(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_SpecializedScalar(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_SpecializedScalar(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string QuantityOperationResultText_Vector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_Vector(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_Vector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string QuantityOperationOtherText_Vector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_Vector(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_Vector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string QuantityOperationResultText_SpecializedVector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_SpecializedVector(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_SpecializedVector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string QuantityOperationOtherText_SpecializedVector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_SpecializedVector(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_SpecializedVector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string QuantityOperationResultText_VectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_VectorGroup(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_VectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string QuantityOperationOtherText_VectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_VectorGroup(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_VectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string QuantityOperationResultText_SpecializedVectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_SpecializedVectorGroup(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_SpecializedVectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string QuantityOperationOtherText_SpecializedVectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_SpecializedVectorGroup(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_SpecializedVectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string QuantityOperationResultText_VectorGroupMember(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation({{quantityType}}, typeof(Length), OperatorType.Addition)]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationResult_VectorGroupMember(SourceSubtext quantityType)
    {
        var source = QuantityOperationResultText_VectorGroupMember(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static string QuantityOperationOtherText_VectorGroupMember(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), {{quantityType}}, OperatorType.Addition)]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOther_VectorGroupMember(SourceSubtext quantityType)
    {
        var source = QuantityOperationOtherText_VectorGroupMember(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "QuantityOperation(typeof(Length), "));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static string VectorOperationText_Vector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [VectorOperation({{quantityType}}, typeof(Position3), VectorOperatorType.Dot)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_Vector(SourceSubtext quantityType)
    {
        var source = VectorOperationText_Vector(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string VectorOperationText_SpecializedVector(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;

        [VectorOperation({{quantityType}}, typeof(Position3), VectorOperatorType.Dot)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorOperationText_VectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation({{quantityType}}, typeof(Position3), VectorOperatorType.Dot)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroup(SourceSubtext quantityType)
    {
        var source = VectorOperationText_VectorGroup(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string VectorOperationText_SpecializedVectorGroup(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation({{quantityType}}, typeof(Position), VectorOperatorType.Dot)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorOperationText_VectorGroupMember(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation({{quantityType}}, typeof(Position3), VectorOperatorType.Dot)]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroupMember(SourceSubtext quantityType)
    {
        var source = VectorOperationText_VectorGroupMember(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "VectorOperation("));

        return AssertExactlyTypeNotQuantityDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

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
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

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
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
