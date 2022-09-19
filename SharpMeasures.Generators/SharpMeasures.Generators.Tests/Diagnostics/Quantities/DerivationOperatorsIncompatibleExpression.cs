﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DerivationOperatorsIncompatibleExpression
{
    [Fact]
    public Task VerifyDerivationOperatorsIncompatibleExpressionDiagnosticsMessage() => AssertScalar(OneElementTwiceAndOtherOnceToScalar).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(ScalarResultingInvalidExpressions))]
    public void Scalar(TextConfig config) => AssertScalar(config);

    [Theory]
    [MemberData(nameof(ScalarResultingInvalidExpressions))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(VectorResultingInvalidExpressions))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(VectorResultingInvalidExpressions))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(VectorResultingInvalidExpressions))]
    public void VectorGroup(TextConfig config) => AssertVectorGroup(config);

    [Theory]
    [MemberData(nameof(VectorResultingInvalidExpressions))]
    public void SpecializedVectorGroup(TextConfig config) => AssertSpecializedVectorGroup(config);

    [Theory]
    [MemberData(nameof(VectorResultingInvalidExpressions))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    public static IEnumerable<object[]> ScalarResultingInvalidExpressions() => new object[][]
    {
        new object[] { OneElementOnceToScalar },
        new object[] { OneElementTwiceToScalar },
        new object[] { OneElementTwiceAndOtherOnceToScalar }
    };

    public static IEnumerable<object[]> VectorResultingInvalidExpressions() => new object[][]
    {
        new object[] { OneElementOnceToVector },
        new object[] { OneElementTwiceToVector },
        new object[] { OneElementTwiceAndOtherOnceToVector }
    };

    private static TextConfig OneElementOnceToScalar => new("\"{0}\"", "typeof(Height)");
    private static TextConfig OneElementTwiceToScalar => new("\"{0} * {0}\"", "typeof(Height)");
    private static TextConfig OneElementTwiceAndOtherOnceToScalar => new("\"{0} * {0} / {1}\"", "typeof(Height), typeof(Distance)");

    private static TextConfig OneElementOnceToVector => new("\"{0}\"", "typeof(Size3)");
    private static TextConfig OneElementTwiceToVector => new("\"{0} + {0}\"", "typeof(Size3)");
    private static TextConfig OneElementTwiceAndOtherOnceToVector => new("\"{0} * {0} * {1}\"", "typeof(Height), typeof(Size3)");

    public readonly record struct TextConfig(string Expression, string Signature);

    private static GeneratorVerifier AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DerivationOperatorsIncompatibleExpressionDiagnostics);
    private static IReadOnlyCollection<string> DerivationOperatorsIncompatibleExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.DerivationOperatorsIncompatibleExpression };

    private static string ScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig config)
    {
        var source = ScalarText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(config));
    }

    private static string SpecializedScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(config));
    }

    private static string VectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(config));
    }

    private static string SpecializedVectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(config));
    }

    private static string VectorGroupText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical(config));
    }

    private static string SpecializedVectorGroupText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical(config));
    }

    private static string VectorGroupMemberText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [DerivedQuantity({{config.Expression}}, {{config.Signature}}, OperatorImplementation = DerivationOperatorImplementation.All)]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.All", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(config));
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
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

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
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

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
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

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
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

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
        public partial class Height { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

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
