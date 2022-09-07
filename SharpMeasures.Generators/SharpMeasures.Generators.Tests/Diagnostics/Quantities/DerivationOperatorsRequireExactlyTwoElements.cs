namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DerivationOperatorsRequireExactlyTwoElements
{
    [Fact]
    public Task VerifyDerivationOperatorsRequireExactlyTwoElementsDiagnosticsMessage() => AssertScalar(OneElementSignature).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void Scalar(string signature) => AssertScalar(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void SpecializedScalar(string signature) => AssertSpecializedScalar(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void Vector(string signature) => AssertVector(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void SpecializedVector(string signature) => AssertSpecializedVector(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void VectorGroup(string signature) => AssertVectorGroup(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void SpecializedVectorGroup(string signature) => AssertSpecializedVectorGroup(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void VectorGroupMember(string signature) => AssertVectorGroupMember(signature);

    public static IEnumerable<object[]> InvalidSignatureLengths() => new object[][]
    {
        new[] { OneElementSignature },
        new[] { ThreeElementsSignature }
    };

    private static string OneElementSignature => "typeof(Height)";
    private static string ThreeElementsSignature => "typeof(Height), typeof(Altitude), typeof(Distance)";

    private static GeneratorVerifier AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DerivationOperatorsRequireExactlyTwoElementsDiagnostics);
    private static IReadOnlyCollection<string> DerivationOperatorsRequireExactlyTwoElementsDiagnostics { get; } = new string[] { DiagnosticIDs.DerivationOperatorsRequireExactlyTwoElements };

    private static string ScalarText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(string signature)
    {
        var source = ScalarText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(signature));
    }

    private static string SpecializedScalarText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresScalar(typeof(Length2))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length2 { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(string signature)
    {
        var source = SpecializedScalarText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(signature));
    }

    private static string VectorText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string signature)
    {
        var source = VectorText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(signature));
    }

    private static string SpecializedVectorText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(string signature)
    {
        var source = SpecializedVectorText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(signature));
    }

    private static string VectorGroupText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(string signature)
    {
        var source = VectorGroupText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical(signature));
    }

    private static string SpecializedVectorGroupText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(string signature)
    {
        var source = SpecializedVectorGroupText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical(signature));
    }

    private static string VectorGroupMemberText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string signature)
    {
        var source = VectorGroupMemberText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(signature));
    }

    private static GeneratorVerifier ScalarIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(signature));
    private static GeneratorVerifier SpecializedScalarIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(signature));
    private static GeneratorVerifier VectorIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText(signature));
    private static GeneratorVerifier SpecializedVectorIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText(signature));
    private static GeneratorVerifier VectorGroupIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText(signature));
    private static GeneratorVerifier SpecializedVectorGroupIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText(signature));
    private static GeneratorVerifier VectorGroupMemberIdentical(string signature) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText(signature));

    private static string ScalarIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresScalar(typeof(Length2))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length2 { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText(string signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity("{0}", {{signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
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
