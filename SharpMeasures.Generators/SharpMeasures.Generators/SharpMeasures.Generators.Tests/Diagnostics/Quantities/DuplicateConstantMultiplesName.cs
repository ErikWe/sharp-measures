namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateConstantMultiplesName
{
    [Fact]
    public Task VerifyDuplicateConstantMultiplesNameDiagnosticsMessage() => AssertScalar(ScalarBothExplicit.FirstArgument, ScalarBothExplicit.SecondArgument, ScalarBothExplicit.TargetAttribute).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(DuplicateScalarConstantMultiplesNames))]
    public void Scalar(string firstArgument, SourceSubtext secondArgument, bool targetAttribute) => AssertScalar(firstArgument, secondArgument, targetAttribute);

    [Theory]
    [MemberData(nameof(DuplicateScalarConstantMultiplesNames))]
    public void SpecializedScalar(string firstArgument, SourceSubtext secondArgument, bool targetAttribute) => AssertSpecializedScalar(firstArgument, secondArgument, targetAttribute);

    [Theory]
    [MemberData(nameof(DuplicateVectorConstantMultiplesNames))]
    public void Vector(string firstArgument, SourceSubtext secondArgument, bool targetAttribute) => AssertVector(firstArgument, secondArgument, targetAttribute);

    [Theory]
    [MemberData(nameof(DuplicateVectorConstantMultiplesNames))]
    public void SpecializedVector(string firstArgument, SourceSubtext secondArgument, bool targetAttribute) => AssertSpecializedVector(firstArgument, secondArgument, targetAttribute);

    [Theory]
    [MemberData(nameof(DuplicateVectorConstantMultiplesNames))]
    public void VectorGroupMember(string firstArgument, SourceSubtext secondArgument, bool targetAttribute) => AssertVectorGroupMember(firstArgument, secondArgument, targetAttribute);

    public static IEnumerable<object[]> DuplicateScalarConstantMultiplesNames() => new object[][]
    {
        new object[] { ScalarBothExplicit.FirstArgument, ScalarBothExplicit.SecondArgument, ScalarBothExplicit.TargetAttribute },
        new object[] { ScalarFirstExplicit.FirstArgument, ScalarFirstExplicit.SecondArgument, ScalarFirstExplicit.TargetAttribute },
        new object[] { ScalarSecondExplicit.FirstArgument, ScalarSecondExplicit.SecondArgument, ScalarSecondExplicit.TargetAttribute }
    };

    public static IEnumerable<object[]> DuplicateVectorConstantMultiplesNames() => new object[][]
    {
        new object[] { VectorBothExplicit.FirstArgument, VectorBothExplicit.SecondArgument, VectorBothExplicit.TargetAttribute },
        new object[] { VectorFirstExplicit.FirstArgument, VectorFirstExplicit.SecondArgument, VectorFirstExplicit.TargetAttribute },
        new object[] { VectorSecondExplicit.FirstArgument, VectorSecondExplicit.SecondArgument, VectorSecondExplicit.TargetAttribute }
    };

    private static (string FirstArgument, SourceSubtext SecondArgument, bool TargetAttribute) ScalarBothExplicit => (ScalarFirstAttributeMultiples.Text, ScalarFirstAttributeMultiples, false);
    private static (string FirstArgument, SourceSubtext SecondArgument, bool TargetAttribute) ScalarFirstExplicit => (string.Empty, ScalarFirstAttributeMultiples, false);
    private static (string FirstArgument, SourceSubtext SecondArgument, bool TargetAttribute) ScalarSecondExplicit => (ScalarSecondAttributeMultiples.Text, SourceSubtext.Covered(string.Empty), true);

    private static (string FirstArgument, SourceSubtext SecondArgument, bool TargetAttribute) VectorBothExplicit => (VectorFirstAttributeMultiples.Text, VectorFirstAttributeMultiples, false);
    private static (string FirstArgument, SourceSubtext SecondArgument, bool TargetAttribute) VectorFirstExplicit => (string.Empty, VectorFirstAttributeMultiples, false);
    private static (string FirstArgument, SourceSubtext SecondArgument, bool TargetAttribute) VectorSecondExplicit => (VectorSecondAttributeMultiples.Text, SourceSubtext.Covered(string.Empty), true);

    private static SourceSubtext ScalarFirstAttributeMultiples { get; } = SourceSubtext.Covered("\"MultiplesOfPlanck\"", prefix: ", Multiples = ");
    private static SourceSubtext ScalarSecondAttributeMultiples { get; } = SourceSubtext.Covered("\"MultiplesOfPlanck2\"", prefix: ", Multiples = ");
    private static SourceSubtext VectorFirstAttributeMultiples { get; } = SourceSubtext.Covered("\"MultiplesOfMetreOnes\"", prefix: ", Multiples = ");
    private static SourceSubtext VectorSecondAttributeMultiples { get; } = SourceSubtext.Covered("\"MultiplesOfMetreOnes2\"", prefix: ", Multiples = ");

    private static GeneratorVerifier AssertExactlyDuplicateConstantMultiplesNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantMultiplesNameDiagnostics);
    private static IReadOnlyCollection<string> DuplicateConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantMultiplesName };

    private static string ScalarText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(string firstArgument, SourceSubtext secondArgument, bool targetAttribute)
    {
        var source = ScalarText(firstArgument, secondArgument.Text);

        var expectedLocation = targetAttribute
            ? ExpectedDiagnosticsLocation.TextSpan(source, target: "ScalarConstant", postfix: "(\"Planck2\"")
            : ExpectedDiagnosticsLocation.TextSpan(source, secondArgument.Context.With(outerPrefix: "(\"Planck2\", \"Metre\", 1.616255E-35"));

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(string firstArgument, SourceSubtext secondArgument, bool targetAttribute)
    {
        var source = SpecializedScalarText(firstArgument, secondArgument.Text);

        var expectedLocation = targetAttribute
            ? ExpectedDiagnosticsLocation.TextSpan(source, target: "ScalarConstant", postfix: "(\"Planck2\"")
            : ExpectedDiagnosticsLocation.TextSpan(source, secondArgument.Context.With(outerPrefix: "(\"Planck2\", \"Metre\", 1.616255E-35"));

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1{{firstArgument}})]
        [VectorConstant("MetreOnes2", "Metre", 1, 1, 1{{secondArgument}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string firstArgument, SourceSubtext secondArgument, bool targetAttribute)
    {
        var source = VectorText(firstArgument, secondArgument.Text);

        var expectedLocation = targetAttribute
            ? ExpectedDiagnosticsLocation.TextSpan(source, target: "VectorConstant", postfix: "(\"MetreOnes2\"")
            : ExpectedDiagnosticsLocation.TextSpan(source, secondArgument.Context.With(outerPrefix: "(\"MetreOnes2\", \"Metre\", 1, 1, 1"));

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1{{firstArgument}})]
        [VectorConstant("MetreOnes2", "Metre", 1, 1, 1{{secondArgument}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(string firstArgument, SourceSubtext secondArgument, bool targetAttribute)
    {
        var source = SpecializedVectorText(firstArgument, secondArgument.Text);

        var expectedLocation = targetAttribute
            ? ExpectedDiagnosticsLocation.TextSpan(source, target: "VectorConstant", postfix: "(\"MetreOnes2\"")
            : ExpectedDiagnosticsLocation.TextSpan(source, secondArgument.Context.With(outerPrefix: "(\"MetreOnes2\", \"Metre\", 1, 1, 1"));

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupMemberText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1{{firstArgument}})]
        [VectorConstant("MetreOnes2", "Metre", 1, 1, 1{{secondArgument}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string firstArgument, SourceSubtext secondArgument, bool targetAttribute)
    {
        var source = VectorGroupMemberText(firstArgument, secondArgument.Text);

        var expectedLocation = targetAttribute
            ? ExpectedDiagnosticsLocation.TextSpan(source, target: "VectorConstant", postfix: "(\"MetreOnes2\"")
            : ExpectedDiagnosticsLocation.TextSpan(source, secondArgument.Context.With(outerPrefix: "(\"MetreOnes2\", \"Metre\", 1, 1, 1"));

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
