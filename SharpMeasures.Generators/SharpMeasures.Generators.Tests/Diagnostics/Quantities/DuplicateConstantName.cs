namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateConstantName
{
    [Fact]
    public Task VerifyDuplicateConstantName_SingularWithSingular() => AssertScalar(SingularWithSingular).VerifyDiagnostics();

    [Fact]
    public Task VerifyDuplicateConstantName_SingularWithMultiples() => AssertScalar(SingularWithExplicitMultiples).VerifyDiagnostics();

    [Fact]
    public Task VerifyDuplicateConstantName_MultiplesWithSingular() => AssertScalar(ExplicitMultiplesWithSingular).VerifyDiagnostics();

    [Fact]
    public Task VerifyDuplicateConstantName_MultiplesWithMultiples() => AssertScalar(ExplicitMultiplesWithMultiples).VerifyDiagnostics();

    [Fact]
    public Task VerifyDuplicateConstantName_SameConstant() => AssertScalar(SameConstant).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void Scalar(TextConfig config) => AssertScalar(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    public static IEnumerable<object[]> DuplicateNames => new object[][]
    {
        new object[] { SingularWithSingular },
        new object[] { SingularWithExplicitMultiples },
        new object[] { SingularWithImplicitMultiples },
        new object[] { ExplicitMultiplesWithSingular },
        new object[] { ExplicitMultiplesWithMultiples },
        new object[] { ImplicitMultiplesWithSingular },
        new object[] { ImplicitMultiplesWithMultiples }
    };

    private static TextConfig SingularWithSingular { get; } = new("Kilometre", string.Empty, "Kilometre", string.Empty, DiagnosticsTarget.Singular);
    private static TextConfig SingularWithExplicitMultiples { get; } = new("Kilometre", "Kilometres", "Kilometres", string.Empty, DiagnosticsTarget.Singular);
    private static TextConfig SingularWithImplicitMultiples { get; } = new("Kilometre", string.Empty, "MultiplesOfKilometre", string.Empty, DiagnosticsTarget.Singular);
    private static TextConfig ExplicitMultiplesWithSingular { get; } = new("Kilometre", string.Empty, "Kilometre2", "Kilometre", DiagnosticsTarget.Multiples);
    private static TextConfig ExplicitMultiplesWithMultiples { get; } = new("Kilometre", string.Empty, "Kilometre2", "MultiplesOfKilometre", DiagnosticsTarget.Multiples);
    private static TextConfig ImplicitMultiplesWithSingular { get; } = new("MultiplesOfKilometre", string.Empty, "Kilometre", string.Empty, DiagnosticsTarget.Attribute);
    private static TextConfig ImplicitMultiplesWithMultiples { get; } = new("Kilometre2", "MultiplesOfKilometre", "Kilometre", string.Empty, DiagnosticsTarget.Attribute);
    private static TextConfig SameConstant { get; } = new("Kilometre2", string.Empty, "Kilometre", "Kilometre", DiagnosticsTarget.Attribute);

    [SuppressMessage("Design", "CA1034", Justification = "Test-method argument")]
    public readonly record struct TextConfig(string FirstSingular, string FirstMultiples, string SecondSingular, string SecondMultiples, DiagnosticsTarget Target);
    public enum DiagnosticsTarget { Singular, Multiples, Attribute }

    private static GeneratorVerifier AssertExactlyDuplicateConstantNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantNameDiagnostics);
    private static IReadOnlyCollection<string> DuplicateConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantName };

    private static TextSpan ParseExpectedLocation(string source, TextConfig config, string attribute) => config.Target switch
    {
        DiagnosticsTarget.Singular => ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.SecondSingular}\"", postfix: ", \"Meter\""),
        DiagnosticsTarget.Multiples => ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.SecondMultiples}\"", prefix: $"\"Meter\", {(attribute is "ScalarConstant" ? "1000" : "1, 1, 1")}, Multiples = "),
        DiagnosticsTarget.Attribute => ExpectedDiagnosticsLocation.TextSpan(source, target: attribute, postfix: $"(\"{config.SecondSingular}\", \"Meter\""),
        _ => throw new ArgumentException($"{config.Target} is not a valid {typeof(DiagnosticsTarget).Name}")
    };

    private static string ScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [ScalarConstant("{{config.SecondSingular}}", "Meter", 1000{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig config)
    {
        var source = ScalarText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [ScalarConstant("{{config.SecondSingular}}", "Meter", 1000{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig config)
    {
        var source = SpecializedScalarText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [VectorConstant("{{config.SecondSingular}}", "Meter", 1, 1, 1{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig config)
    {
        var source = VectorText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [VectorConstant("{{config.SecondSingular}}", "Meter", 1, 1, 1{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig config)
    {
        var source = SpecializedVectorText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupMemberText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [VectorConstant("{{config.SecondSingular}}", "Meter", 1, 1, 1{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig config)
    {
        var source = VectorGroupMemberText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
