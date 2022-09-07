namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

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
    public Task VerifyDerivationOperatorsIncompatibleExpressionDiagnosticsMessage() => AssertScalar(OneElementOnce).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void Scalar(string signature) => AssertScalar(signature);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void SpecializedScalar(string signature) => AssertSpecializedScalar(signature);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void Vector(string signature) => AssertVector(signature);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void SpecializedVector(string signature) => AssertSpecializedVector(signature);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void VectorGroup(string signature) => AssertVectorGroup(signature);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void SpecializedVectorGroup(string signature) => AssertSpecializedVectorGroup(signature);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void VectorGroupMember(string signature) => AssertVectorGroupMember(signature);

    public static IEnumerable<object[]> InvalidExpressions() => new object[][]
    {
        new[] { OneElementOnce },
        new[] { SameElementTwice },
        new[] { OneElementTwiceAndOtherOnce }
    };

    private static string OneElementOnce => "\"{0}\"";
    private static string SameElementTwice => "\"{0} * {0}\"";
    private static string OneElementTwiceAndOtherOnce => "\"{0} * {0} / {1}\"";

    private static GeneratorVerifier AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DerivationOperatorsIncompatibleExpressionDiagnostics);
    private static IReadOnlyCollection<string> DerivationOperatorsIncompatibleExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.DerivationOperatorsIncompatibleExpression };

    private static string ScalarText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(string expression)
    {
        var source = ScalarText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(expression));
    }

    private static string SpecializedScalarText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresScalar(typeof(Length2))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length2 { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(string expression)
    {
        var source = SpecializedScalarText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(expression));
    }

    private static string VectorText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string expression)
    {
        var source = VectorText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(expression));
    }

    private static string SpecializedVectorText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(string expression)
    {
        var source = SpecializedVectorText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(expression));
    }

    private static string VectorGroupText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(string expression)
    {
        var source = VectorGroupText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical(expression));
    }

    private static string SpecializedVectorGroupText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(string expression)
    {
        var source = SpecializedVectorGroupText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical(expression));
    }

    private static string VectorGroupMemberText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.Exact)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string expression)
    {
        var source = VectorGroupMemberText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivationOperatorImplementation.Exact", prefix: "OperatorImplementation = ");

        return AssertExactlyDerivationOperatorsIncompatibleExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(expression));
    }

    private static GeneratorVerifier ScalarIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(expression));
    private static GeneratorVerifier SpecializedScalarIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(expression));
    private static GeneratorVerifier VectorIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText(expression));
    private static GeneratorVerifier SpecializedVectorIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText(expression));
    private static GeneratorVerifier VectorGroupIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText(expression));
    private static GeneratorVerifier SpecializedVectorGroupIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText(expression));
    private static GeneratorVerifier VectorGroupMemberIdentical(string expression) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText(expression));

    private static string ScalarIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresScalar(typeof(Length2))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length2 { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText(string expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [DerivedQuantity({{expression}}, typeof(Height), typeof(Distance), OperatorImplementation = DerivationOperatorImplementation.None)]
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
