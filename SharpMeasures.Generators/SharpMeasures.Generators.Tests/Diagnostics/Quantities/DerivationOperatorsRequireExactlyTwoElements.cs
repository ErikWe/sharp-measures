namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public void Scalar(TextConfig config) => AssertScalar(config);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void VectorGroup(TextConfig config) => AssertVectorGroup(config);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void SpecializedVectorGroup(TextConfig config) => AssertSpecializedVectorGroup(config);

    [Theory]
    [MemberData(nameof(InvalidSignatureLengths))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    public static IEnumerable<object[]> InvalidSignatureLengths() => new object[][]
    {
        new object[] { OneElementSignature },
        new object[] { ThreeElementsSignature }
    };

    private static TextConfig OneElementSignature => new("\"{0}\"", "typeof(Height)");
    private static TextConfig ThreeElementsSignature => new("\"{0} * {1} * {2}\"", "typeof(Height), typeof(Altitude), typeof(Distance)");

    [SuppressMessage("Design", "CA1034", Justification = "Test-method argument")]
    public readonly record struct TextConfig(string Expression, string Signature);

    private static GeneratorVerifier AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DerivationOperatorsRequireExactlyTwoElementsDiagnostics);
    private static IReadOnlyCollection<string> DerivationOperatorsRequireExactlyTwoElementsDiagnostics { get; } = new string[] { DiagnosticIDs.DerivationOperatorsRequireExactlyTwoElements };

    private static string ScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig config)
    {
        var source = ScalarText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(config));
    }

    private static string SpecializedScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresScalar(typeof(Length2))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length2 { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig config)
    {
        var source = SpecializedScalarText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(config));
    }

    private static string VectorText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig config)
    {
        var source = VectorText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(config));
    }

    private static string SpecializedVectorText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig config)
    {
        var source = SpecializedVectorText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(config));
    }

    private static string VectorGroupText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(TextConfig config)
    {
        var source = VectorGroupText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical(config));
    }

    private static string SpecializedVectorGroupText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(TextConfig config)
    {
        var source = SpecializedVectorGroupText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical(config));
    }

    private static string VectorGroupMemberText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig config)
    {
        var source = VectorGroupMemberText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsRequireExactlyTwoElementsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(config));
    }

    private static GeneratorVerifier ScalarIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(config));
    private static GeneratorVerifier SpecializedScalarIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(config));
    private static GeneratorVerifier VectorIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText(config));
    private static GeneratorVerifier SpecializedVectorIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText(config));
    private static GeneratorVerifier VectorGroupIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText(config));
    private static GeneratorVerifier SpecializedVectorGroupIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText(config));
    private static GeneratorVerifier VectorGroupMemberIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText(config));

    private static string ScalarIdenticalText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Altitude { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresScalar(typeof(Length2))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length2 { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText(TextConfig config) => $$"""
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

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.None)]
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
