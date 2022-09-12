namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
    public Task VerifyDuplicateConstantName_MultiplesWithMultiples() => AssertScalar(ExplicitMultiplesWithExplicitMultiples).VerifyDiagnostics();

    [Fact]
    public Task VerifyDuplicateConstantName_SingleConstantSameNameAndMultiples() => AssertScalar(SingleConstantSameNameAndMultiples).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void Scalar(TextConfig config) => AssertScalar(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SpecializedScalar_Inherited(TextConfig config) => AssertSpecializedScalar_Inherited(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SpecializedVector_Inherited(TextConfig config) => AssertSpecializedVector_Inherited(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorGroupMember_Inherited(TextConfig config) => AssertVectorGroupMember_Inherited(config);

    public static IEnumerable<object[]> DuplicateNames => new object[][]
    {
        new object[] { SingularWithSingular },
        new object[] { SingularWithExplicitMultiples },
        new object[] { SingularWithImplicitMultiples },
        new object[] { ExplicitMultiplesWithSingular },
        new object[] { ExplicitMultiplesWithExplicitMultiples },
        new object[] { ExplicitMultiplesWithImplicitMultiples },
        new object[] { ImplicitMultiplesWithSingular },
        new object[] { ImplicitMultiplesWithExplicitMultiples }
    };

    private static TextConfig SingularWithSingular { get; } = new("Kilometre", string.Empty, "Kilometre", string.Empty, DiagnosticsTarget.Singular);
    private static TextConfig SingularWithExplicitMultiples { get; } = new("Kilometre", "Kilometres", "Kilometres", string.Empty, DiagnosticsTarget.Singular);
    private static TextConfig SingularWithImplicitMultiples { get; } = new("Kilometre", string.Empty, "MultiplesOfKilometre", string.Empty, DiagnosticsTarget.Singular);
    private static TextConfig ExplicitMultiplesWithSingular { get; } = new("Kilometre", string.Empty, "Kilometre2", "Kilometre", DiagnosticsTarget.Multiples);
    private static TextConfig ExplicitMultiplesWithExplicitMultiples { get; } = new("Kilometre", "MultiplesOfKilometre", "Kilometre2", "MultiplesOfKilometre", DiagnosticsTarget.Multiples);
    private static TextConfig ExplicitMultiplesWithImplicitMultiples { get; } = new("Kilometre", string.Empty, "Kilometre2", "MultiplesOfKilometre", DiagnosticsTarget.Multiples);
    private static TextConfig ImplicitMultiplesWithSingular { get; } = new("MultiplesOfKilometre", string.Empty, "Kilometre", string.Empty, DiagnosticsTarget.Attribute);
    private static TextConfig ImplicitMultiplesWithExplicitMultiples { get; } = new("Kilometre2", "MultiplesOfKilometre", "Kilometre", string.Empty, DiagnosticsTarget.Attribute);
    private static TextConfig SingleConstantSameNameAndMultiples { get; } = new("Kilometre2", string.Empty, "Kilometre", "Kilometre", DiagnosticsTarget.Attribute);

    public readonly record struct TextConfig(string FirstSingular, string FirstMultiples, string SecondSingular, string SecondMultiples, DiagnosticsTarget Target);
    public enum DiagnosticsTarget { Singular, Multiples, Attribute }

    private static GeneratorVerifier AssertExactlyDuplicateConstantNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantNameDiagnostics);
    private static IReadOnlyCollection<string> DuplicateConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantName };

    private static TextSpan ParseExpectedLocation(string source, TextConfig config, string attribute) => config.Target switch
    {
        DiagnosticsTarget.Singular => ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.SecondSingular}\"", postfix: ", \"Meter\""),
        DiagnosticsTarget.Multiples => ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.SecondMultiples}\"", prefix: $"\"Meter\", {(attribute is "ScalarConstant" ? "1001" : "2, 2, 2")}, Multiples = "),
        DiagnosticsTarget.Attribute => ExpectedDiagnosticsLocation.TextSpan(source, target: attribute, postfix: $"(\"{config.SecondSingular}\", \"Meter\""),
        _ => throw new ArgumentException($"{config.Target} is not a valid {typeof(DiagnosticsTarget).Name}")
    };

    private static string ScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [ScalarConstant("{{config.SecondSingular}}", "Meter", 1001{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig config)
    {
        var source = ScalarText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(config));
    }

    private static string SpecializedScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [ScalarConstant("{{config.SecondSingular}}", "Meter", 1001{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig config)
    {
        var source = SpecializedScalarText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(config));
    }

    private static string SpecializedScalarText_Inherited(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("{{config.SecondSingular}}", "Meter", 1001{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_Inherited(TextConfig config)
    {
        var source = SpecializedScalarText_Inherited(config);
        var expectedLocation = ParseExpectedLocation(source, config, "ScalarConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical_Inherited(config));
    }

    private static string VectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig config)
    {
        var source = VectorText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(config));
    }

    private static string SpecializedVectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig config)
    {
        var source = SpecializedVectorText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(config));
    }

    private static string SpecializedVectorText_Inherited(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_Inherited(TextConfig config)
    {
        var source = SpecializedVectorText_Inherited(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical_Inherited(config));
    }

    private static string VectorGroupMemberText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig config)
    {
        var source = VectorGroupMemberText(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(config));
    }

    private static string VectorGroupMemberText_Inherited(TextConfig config) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2{{(config.SecondMultiples.Length > 0 ? $", Multiples = \"{config.SecondMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember_Inherited(TextConfig config)
    {
        var source = VectorGroupMemberText_Inherited(config);
        var expectedLocation = ParseExpectedLocation(source, config, "VectorConstant");

        return AssertExactlyDuplicateConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_Inherited(config));
    }

    private static GeneratorVerifier ScalarIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(config));
    private static GeneratorVerifier SpecializedScalarIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(config));
    private static GeneratorVerifier SpecializedScalarIdentical_Inherited(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText_Inherited(config));
    private static GeneratorVerifier VectorIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText(config));
    private static GeneratorVerifier SpecializedVectorIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText(config));
    private static GeneratorVerifier SpecializedVectorIdentical_Inherited(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText_Inherited(config));
    private static GeneratorVerifier VectorGroupMemberIdentical(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText(config));
    private static GeneratorVerifier VectorGroupMemberIdentical_Inherited(TextConfig config) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_Inherited(config));

    private static string ScalarIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            """);
        
        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[ScalarConstant("{{config.SecondSingular}}", "Meter", 1001, GenerateMultiplesProperty = false)]""");
        }
        
        source.AppendLine("""
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string SpecializedScalarIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[ScalarConstant("{{config.SecondSingular}}", "Meter", 1001, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine("""
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Distance { }
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
                
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string SpecializedScalarIdenticalText_Inherited(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[ScalarConstant("{{config.SecondSingular}}", "Meter", 1001, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Distance { }

            [ScalarConstant("{{config.FirstSingular}}", "Metre", 1000{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string VectorIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;
            
            [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine($$"""
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string SpecializedVectorIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;
            
            [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine($$"""
            [SpecializedSharpMeasuresVector(typeof(Position3))]
            public partial class Displacement3 { }
            
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
                
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string SpecializedVectorIdenticalText_Inherited(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SpecializedSharpMeasuresVector(typeof(Position3))]
            public partial class Displacement3 { }

            [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
                
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string VectorGroupMemberIdenticalText(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;
            
            [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine($$"""
            [SharpMeasuresVectorGroupMember(typeof(Position))]
            public partial class Position3 { }
            
            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
                
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }

    private static string VectorGroupMemberIdenticalText_Inherited(TextConfig config)
    {
        StringBuilder source = new();

        source.AppendLine("""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            """);

        if (config.Target is not DiagnosticsTarget.Singular)
        {
            source.AppendLine(CultureInfo.InvariantCulture, $$"""[VectorConstant("{{config.SecondSingular}}", "Meter", 2, 2, 2, GenerateMultiplesProperty = false)]""");
        }

        source.AppendLine(CultureInfo.InvariantCulture, $$"""
            [SharpMeasuresVectorGroupMember(typeof(Displacement))]
            public partial class Displacement3 { }
            
            [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
            public static partial class Displacement { }

            [VectorConstant("{{config.FirstSingular}}", "Metre", 1, 1, 1{{(config.FirstMultiples.Length > 0 ? $", Multiples = \"{config.FirstMultiples}\"" : string.Empty)}})]
            [SharpMeasuresVectorGroupMember(typeof(Position))]
            public partial class Position3 { }
            
            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
                
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """);

        return source.ToString();
    }
}
