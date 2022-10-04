namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidQuantityOperationName
{
    [Fact]
    public Task VerifyInvalidQuantityOperationNameDiagnosticsMessage_Null() => AssertQuantityOperation_Scalar(NullName, mirrored: false).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_Scalar(SourceSubtext operationName) => AssertQuantityOperation_Scalar(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_Scalar(SourceSubtext operationName) => AssertQuantityOperation_Scalar(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_SpecializedScalar(SourceSubtext operationName) => AssertQuantityOperation_SpecializedScalar(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_SpecializedScalar(SourceSubtext operationName) => AssertQuantityOperation_SpecializedScalar(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_Vector(SourceSubtext operationName) => AssertQuantityOperation_Vector(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_Vector(SourceSubtext operationName) => AssertQuantityOperation_Vector(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void VectorOperation_Vector(SourceSubtext operationName) => AssertVectorOperation_Vector(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredVectorOperation_Vector(SourceSubtext operationName) => AssertVectorOperation_Vector(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_SpecializedVector(SourceSubtext operationName) => AssertQuantityOperation_SpecializedVector(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_SpecializedVector(SourceSubtext operationName) => AssertQuantityOperation_SpecializedVector(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void VectorOperation_SpecializedVector(SourceSubtext operationName) => AssertVectorOperation_SpecializedVector(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredVectorOperation_SpecializedVector(SourceSubtext operationName) => AssertVectorOperation_SpecializedVector(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_VectorGroup(SourceSubtext operationName) => AssertQuantityOperation_VectorGroup(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_VectorGroup(SourceSubtext operationName) => AssertQuantityOperation_VectorGroup(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void VectorOperation_VectorGroup(SourceSubtext operationName) => AssertVectorOperation_VectorGroup(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredVectorOperation_VectorGroup(SourceSubtext operationName) => AssertVectorOperation_VectorGroup(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_SpecializedVectorGroup(SourceSubtext operationName) => AssertQuantityOperation_SpecializedVectorGroup(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_SpecializedVectorGroup(SourceSubtext operationName) => AssertQuantityOperation_SpecializedVectorGroup(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void VectorOperation_SpecializedVectorGroup(SourceSubtext operationName) => AssertVectorOperation_SpecializedVectorGroup(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredVectorOperation_SpecializedVectorGroup(SourceSubtext operationName) => AssertVectorOperation_SpecializedVectorGroup(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void QuantityOperation_VectorGroupMember(SourceSubtext operationName) => AssertQuantityOperation_VectorGroupMember(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredQuantityOperation_VectorGroupMember(SourceSubtext operationName) => AssertQuantityOperation_VectorGroupMember(operationName, mirrored: true);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void VectorOperation_VectorGroupMember(SourceSubtext operationName) => AssertVectorOperation_VectorGroupMember(operationName, mirrored: false);

    [Theory]
    [MemberData(nameof(InvalidQuantityOperationNames))]
    public void MirroredVectorOperation_VectorGroupMember(SourceSubtext operationName) => AssertVectorOperation_VectorGroupMember(operationName, mirrored: true);

    public static IEnumerable<object[]> InvalidQuantityOperationNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidQuantityOperationNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidQuantityOperationNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidQuantityOperationNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidQuantityOperationName };

    private static string QuantityOperationText_Scalar(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), typeof(Distance), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_Scalar(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_Scalar(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string QuantityOperationText_SpecializedScalar(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_SpecializedScalar(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_SpecializedScalar(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string QuantityOperationText_Vector(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_Vector(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_Vector(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string VectorOperationText_Vector(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Displacement3), VectorOperatorType.Dot, {{(mirrored ? "Mirrored" : string.Empty)}}Name = {{operationName}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_Vector(SourceSubtext operationName, bool mirrored)
    {
        var source = VectorOperationText_Vector(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "Name = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string QuantityOperationText_SpecializedVector(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_SpecializedVector(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_SpecializedVector(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorOperationText_SpecializedVector(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Position3), VectorOperatorType.Dot, {{(mirrored ? "Mirrored" : string.Empty)}}Name = {{operationName}})]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_SpecializedVector(SourceSubtext operationName, bool mirrored)
    {
        var source = VectorOperationText_SpecializedVector(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "Name = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string QuantityOperationText_VectorGroup(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_VectorGroup(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_VectorGroup(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string VectorOperationText_VectorGroup(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Displacement), VectorOperatorType.Dot, {{(mirrored ? "Mirrored" : string.Empty)}}Name = {{operationName}})]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroup(SourceSubtext operationName, bool mirrored)
    {
        var source = VectorOperationText_VectorGroup(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "Name = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string QuantityOperationText_SpecializedVectorGroup(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_SpecializedVectorGroup(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_SpecializedVectorGroup(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorOperationText_SpecializedVectorGroup(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Position), VectorOperatorType.Dot, {{(mirrored ? "Mirrored" : string.Empty)}}Name = {{operationName}})]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_SpecializedVectorGroup(SourceSubtext operationName, bool mirrored)
    {
        var source = VectorOperationText_SpecializedVectorGroup(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "Name = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string QuantityOperationText_VectorGroupMember(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = {{operationName}})]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_VectorGroupMember(SourceSubtext operationName, bool mirrored)
    {
        var source = QuantityOperationText_VectorGroupMember(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "MethodName = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static string VectorOperationText_VectorGroupMember(SourceSubtext operationName, bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Position), VectorOperatorType.Dot, {{(mirrored ? "Mirrored" : string.Empty)}}Name = {{operationName}})]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroupMember(SourceSubtext operationName, bool mirrored)
    {
        var source = VectorOperationText_VectorGroupMember(operationName, mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operationName.Context.With(outerPrefix: "Name = "));

        return AssertExactlyInvalidQuantityOperationNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
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
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

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
        public partial class Position3 { }
        
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
        public static partial class Position { }
        
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

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
