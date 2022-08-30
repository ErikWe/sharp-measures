namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
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
    public void Scalar_ExplicitlyIncluded(TextConfig config) => AssertScalar_ExplicitlyIncluded(config);

    [Theory]
    [MemberData(nameof(ScalarSharedNames))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(ScalarSharedNames))]
    public void SpecializedScalar_ExplicitlyIncluded(TextConfig config) => AssertSpecializedScalar_ExplicitlyIncluded(config);

    [Theory]
    [MemberData(nameof(ScalarSharedNames))]
    public void SpecializedScalar_ExplicitlyIncludedInBase(TextConfig config) => AssertSpecializedScalar_ExplicitlyIncludedInBase(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void Vector_ExplicitlyIncluded(TextConfig config) => AssertVector_ExplicitlyIncluded(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void SpecializedVector_ExplicitlyIncluded(TextConfig config) => AssertSpecializedVector_ExplicitlyIncluded(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void SpecializedVector_ExplicitlyIncludedInBase(TextConfig config) => AssertSpecializedVector_ExplicitlyIncludedInBase(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void VectorGroupMember_ExplicitlyIncluded(TextConfig config) => AssertVectorGroupMember_ExplicitlyIncluded(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void VectorGroupMember_ExplicitlyIncludedInGroup(TextConfig config) => AssertVectorGroupMember_ExplicitlyIncludedInGroup(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void VectorGroupMember_ExplicitlyIncludedInGroupBase(TextConfig config) => AssertVectorGroupMember_ExplicitlyIncludedInGroupBase(config);

    [Theory]
    [MemberData(nameof(VectorSharedNames))]
    public void VectorGroupMember_ExplicitlyIncludedInGroupBaseMember(TextConfig config) => AssertVectorGroupMember_ExplicitlyIncludedInGroupBaseMember(config);

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
    public readonly record struct TextConfig(string ConstantSingular, string ConstantMultiples, string UnitPlural, DiagnosticsTarget Target);
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

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(config));
    }

    private static string ScalarText_ExplicitlyIncluded(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [ScalarConstant("{{config.ConstantSingular}}", "Metre", 1000{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [IncludeUnits("Metre", "Kilometre")]
        [IncludeBases("Metre", "Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar_ExplicitlyIncluded(TextConfig config)
    {
        var source = ScalarText_ExplicitlyIncluded(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(config));
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

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(config));
    }

    private static string SpecializedScalarText_ExplicitlyIncluded(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [ScalarConstant("{{config.ConstantSingular}}", "Metre", 1000{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [IncludeUnits("Metre", "Kilometre")]
        [IncludeBases("Metre", "Kilometre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_ExplicitlyIncluded(TextConfig config)
    {
        var source = SpecializedScalarText_ExplicitlyIncluded(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(config));
    }

    private static string SpecializedScalarText_ExplicitlyIncludedInBase(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [ScalarConstant("{{config.ConstantSingular}}", "Metre", 1000{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [IncludeUnits("Metre", "Kilometre")]
        [IncludeBases("Metre", "Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_ExplicitlyIncludedInBase(TextConfig config)
    {
        var source = SpecializedScalarText_ExplicitlyIncludedInBase(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(config));
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

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(config));
    }

    private static string VectorText_ExplicitlyIncluded(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [IncludeUnits("Metre", "Kilometre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector_ExplicitlyIncluded(TextConfig config)
    {
        var source = VectorText_ExplicitlyIncluded(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(config));
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

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(config));
    }

    private static string SpecializedVectorText_ExplicitlyIncluded(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [IncludeUnits("Metre", "Kilometre")]
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

    private static GeneratorVerifier AssertSpecializedVector_ExplicitlyIncluded(TextConfig config)
    {
        var source = SpecializedVectorText_ExplicitlyIncluded(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(config));
    }

    private static string SpecializedVectorText_ExplicitlyIncludedInBase(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [IncludeUnits("Metre", "Kilometre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_ExplicitlyIncludedInBase(TextConfig config)
    {
        var source = SpecializedVectorText_ExplicitlyIncludedInBase(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(config));
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

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(config));
    }

    private static string VectorGroupMemberText_ExplicitlyIncluded(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [IncludeUnits("Metre", "Kilometre")]
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

    private static GeneratorVerifier AssertVectorGroupMember_ExplicitlyIncluded(TextConfig config)
    {
        var source = VectorGroupMemberText_ExplicitlyIncluded(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(config));
    }

    private static string VectorGroupMemberText_ExplicitlyIncludedInGroup(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [IncludeUnits("Metre", "Kilometre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember_ExplicitlyIncludedInGroup(TextConfig config)
    {
        var source = VectorGroupMemberText_ExplicitlyIncludedInGroup(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(config));
    }

    private static string VectorGroupMemberText_ExplicitlyIncludedInGroupBase(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [IncludeUnits("Metre", "Kilometre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember_ExplicitlyIncludedInGroupBase(TextConfig config)
    {
        var source = VectorGroupMemberText_ExplicitlyIncludedInGroupBase(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberOfSpecializedGroupIdentical(config));
    }

    private static string VectorGroupMemberText_ExplicitlyIncludedInGroupBaseMember(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.ConstantSingular}}", "Metre", 1, 1, 1{{(config.ConstantMultiples.Length > 0 ? $", Multiples = \"{config.ConstantMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [IncludeUnits("Metre", "Kilometre")]
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

    private static GeneratorVerifier AssertVectorGroupMember_ExplicitlyIncludedInGroupBaseMember(TextConfig config)
    {
        var source = VectorGroupMemberText_ExplicitlyIncludedInGroupBaseMember(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyConstantSharesNameWithUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberOfSpecializedGroupIdentical(config));
    }

    private static GeneratorVerifier ScalarIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(config));
    private static GeneratorVerifier SpecializedScalarIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(config));
    private static GeneratorVerifier VectorIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText(config));
    private static GeneratorVerifier SpecializedVectorIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText(config));
    private static GeneratorVerifier VectorGroupMemberIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText(config));
    private static GeneratorVerifier VectorGroupMemberOfSpecializedGroupIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberOfSpecializedGroupIdenticalText(config));

    private static string ScalarIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            
            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $"""[ScalarConstant("{config.ConstantSingular}", "Metre", 1000, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
                
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string SpecializedScalarIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

        """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $"""[ScalarConstant("{config.ConstantSingular}", "Metre", 1000, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Distance { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string VectorIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $"""[VectorConstant("{config.ConstantSingular}", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "{{config.UnitPlural}}", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string SpecializedVectorIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $"""[VectorConstant("{config.ConstantSingular}", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
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
            """);

        return source.ToString();
    }

    private static string VectorGroupMemberIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $"""[VectorConstant("{config.ConstantSingular}", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
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
            """);

        return source.ToString();
    }

    private static string VectorGroupMemberOfSpecializedGroupIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $"""[VectorConstant("{config.ConstantSingular}", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SharpMeasuresVectorGroupMember(typeof(Displacement))]
            public partial class Displacement3 { }

            [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
            public static partial class Displacement { }

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
            """);

        return source.ToString();
    }
}
