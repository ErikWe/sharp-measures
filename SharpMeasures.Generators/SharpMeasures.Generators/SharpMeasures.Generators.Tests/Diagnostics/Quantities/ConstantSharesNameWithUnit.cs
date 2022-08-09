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
public class ConstantSharesNameWithUnit
{
    [Fact]
    public Task VerifyConstantSharesNameWithUnitDiagnosticsMessage() => AssertScalar(SingularWithUnitSingular).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(ScalarSharedNames))]
    public void Scalar(TextConfig config) => AssertScalar(config);

    [Theory]
    [MemberData(nameof(ScalarSharedNames))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    public static IEnumerable<object[]> ScalarSharedNames => new object[][]
    {
        new object[] { SingularWithUnitSingular },
        new object[] { SingularWithUnitPlural },
        new object[] { ExplicitMultiplesWithUnitSingular },
        new object[] { ExplicitMultiplesWithUnitPlural },
        new object[] { ImplicitMultiplesWithUnitPlural }
    };

    public static IEnumerable<object[]> VectorSharedNames => new object[][]
    {
        new object[] { SingularWithUnitPlural },
        new object[] { ExplicitMultiplesWithUnitPlural },
        new object[] { ImplicitMultiplesWithUnitPlural }
    };

    private static TextConfig SingularWithUnitSingular { get; } = new("OneKilometre", string.Empty, "Kilometres", DiagnosticsTarget.Singular);
    private static TextConfig SingularWithUnitPlural { get; } = new("Kilometres", string.Empty, "Kilometres", DiagnosticsTarget.Singular);
    private static TextConfig ExplicitMultiplesWithUnitSingular { get; } = new("Kilometre2", "OneKilometre", "Kilometres", DiagnosticsTarget.Multiples);
    private static TextConfig ExplicitMultiplesWithUnitPlural { get; } = new("Kilometre2", "Kilometres", "Kilometres", DiagnosticsTarget.Multiples);
    private static TextConfig ImplicitMultiplesWithUnitPlural { get; } = new("Kilometre", string.Empty, "MultiplesOfKilometre", DiagnosticsTarget.Attribute);

    [SuppressMessage("Design", "CA1034", Justification = "Test-method argument")]
    public record struct TextConfig(string ConstantSingular, string ConstantMultiples, string UnitPlural, DiagnosticsTarget Target);
    public enum DiagnosticsTarget { Singular, Multiples, Attribute }

    private static GeneratorVerifier AssertExactlyConstantSharesNameWithUnitDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ConstantSharesNameWithUnitDiagnostics);
    private static IReadOnlyCollection<string> ConstantSharesNameWithUnitDiagnostics { get; } = new string[] { DiagnosticIDs.ConstantSharesNameWithUnit };

    private static TextSpan ParseExpectedLocation(string source, TextConfig config, string attribute) => config.Target switch
    {
        DiagnosticsTarget.Singular => ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.ConstantSingular}\"", prefix: $"{attribute}("),
        DiagnosticsTarget.Multiples => ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.ConstantMultiples}\"", prefix: "Multiples = "),
        DiagnosticsTarget.Attribute => ExpectedDiagnosticsLocation.TextSpan(source, target: attribute),
        _ => throw new ArgumentException($"{config.Target} is not a valid {typeof(DiagnosticsTarget).Name}")
    };

    private static string ScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [ScalarConstant("{{config.ConstantSingular}}", "Metre", 1000{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig config)
    {
        var source = ScalarText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [ScalarConstant("{{config.ConstantSingular}}", "Metre", 1000{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig config)
    {
        var source = SpecializedScalarText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ?  $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig config)
    {
        var source = VectorText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig config)
    {
        var source = SpecializedVectorText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupMemberText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig config)
    {
        var source = VectorGroupMemberText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
