namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnexpectedResultFromDerivation
{
    [Fact]
    public Task VerifyUnexpectedResultFromDerivationDiagnosticsMessage_Scalar() => AssertVector(ScalarResult).VerifyDiagnostics();

    [Fact]
    public Task VerifyUnexpectedResultFromDerivationDiagnosticsMessage_Vector() => AssertScalar(VectorResult).VerifyDiagnostics();

    [Fact]
    public Task VerifyUnexpectedResultFromDerivationDiagnosticsMessage_Dimension() => AssertVector(VectorResult).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(IncompatibleWithScalar))]
    public void Scalar(string signature) => AssertScalar(signature);

    [Theory]
    [MemberData(nameof(IncompatibleWithScalar))]
    public void SpecializedScalar(string signature) => AssertSpecializedScalar(signature);

    [Theory]
    [MemberData(nameof(IncompatibleWithVector))]
    public void Vector(string signature) => AssertVector(signature);

    [Theory]
    [MemberData(nameof(IncompatibleWithVector))]
    public void SpecializedVector(string signature) => AssertSpecializedVector(signature);

    [Theory]
    [MemberData(nameof(IncompatibleWithVector))]
    public void VectorGroup(string signature) => AssertVectorGroup(signature);

    [Theory]
    [MemberData(nameof(IncompatibleWithVector))]
    public void SpecializedVectorGroup(string signature) => AssertSpecializedVectorGroup(signature);

    [Theory]
    [MemberData(nameof(IncompatibleWithVector))]
    public void VectorGroupMember(string signature) => AssertVectorGroupMember(signature);

    public static IEnumerable<object[]> IncompatibleWithScalar => new object[][]
    {
        new object[] { VectorResult },
        new object[] { GroupResult }
    };

    public static IEnumerable<object[]> IncompatibleWithVector => new object[][]
    {
        new object[] { ScalarResult },
        new object[] { VectorResult },
        new object[] { GroupResult }
    };

    private static string ScalarResult { get; } = "typeof(Length), typeof(Distance)";
    private static string VectorResult { get; } = "typeof(Position3), typeof(Displacement3)";
    private static string GroupResult { get; } = "typeof(Position), typeof(Displacement)";

    private static GeneratorVerifier AssertExactlyUnexpectedResultFromDerivationDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnexpectedResultFromDerivationDiagnostics);
    private static IReadOnlyCollection<string> UnexpectedResultFromDerivationDiagnostics { get; } = new string[] { DiagnosticIDs.UnexpectedResultFromDerivation };

    private static string ScalarText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class A { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(string signature)
    {
        var source = ScalarText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SpecializedSharpMeasuresScalar(typeof(A))]
        public partial class B { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class A { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(string signature)
    {
        var source = SpecializedScalarText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class A2 { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string signature)
    {
        var source = VectorText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SpecializedSharpMeasuresVector(typeof(A2))]
        public partial class B2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class A2 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(string signature)
    {
        var source = SpecializedVectorText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(A))]
        public partial class A2 { }
        
        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class A { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(string signature)
    {
        var source = VectorGroupText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(B))]
        public partial class B2 { }
        
        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SpecializedSharpMeasuresVectorGroup(typeof(A))]
        public static partial class B { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class A { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(string signature)
    {
        var source = SpecializedVectorGroupText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorGroupMemberText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{0} + {1}", {{signature}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class A2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class A { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string signature)
    {
        var source = VectorGroupMemberText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedQuantity");

        return AssertExactlyUnexpectedResultFromDerivationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class A { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresScalar(typeof(A))]
        public partial class B { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class A { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class A2 { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(A2))]
        public partial class B2 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class A2 { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(A))]
        public partial class A2 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class A { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(B))]
        public partial class B2 { }
        
        [SpecializedSharpMeasuresVectorGroup(typeof(A))]
        public static partial class B { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class A { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class A2 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class A { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position4 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
