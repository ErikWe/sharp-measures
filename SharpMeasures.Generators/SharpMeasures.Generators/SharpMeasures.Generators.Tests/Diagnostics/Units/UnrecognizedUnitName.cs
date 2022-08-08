namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedUnitName
{
    [Fact]
    public Task VerifyUnrecognizedUnitNameDiagnosticsMessage_Null() => AssertUnitAlias(NullName).VerifyDiagnostics();

    [Fact]
    public Task VerifyUnrecognizedUnitNameDiagnosticsMessage_Empty() => AssertUnitAlias(EmptyName).VerifyDiagnostics();

    [Fact]
    public Task VerifyUnrecognizedUnitNameDiagnosticsMessage_Missing() => AssertUnitAlias(MissingName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void UnitAlias(SourceSubtext aliasOf) => AssertUnitAlias(aliasOf);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void BiasedUnit(SourceSubtext from) => AssertBiasedUnit(from);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void DerivedUnit(SourceSubtext signatureElement) => AssertDerivedUnit(signatureElement);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void PrefixedUnit(SourceSubtext from) => AssertPrefixedUnit(from);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScaledUnit(SourceSubtext from) => AssertScaledUnit(from);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarDefaultUnit(SourceSubtext name) => AssertScalarDefaultUnit(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarDefaultUnit(SourceSubtext name) => AssertSpecializedScalarDefaultUnit(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorDefaultUnit(SourceSubtext name) => AssertVectorDefaultUnit(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorDefaultUnit(SourceSubtext name) => AssertSpecializedVectorDefaultUnit(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupDefaultUnit(SourceSubtext name) => AssertVectorGroupDefaultUnit(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorGroupDefaultUnit(SourceSubtext name) => AssertSpecializedVectorGroupDefaultUnit(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarConstant(SourceSubtext name) => AssertScalarConstant(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarConstant(SourceSubtext name) => AssertSpecializedScalarConstant(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorConstant(SourceSubtext name) => AssertVectorConstant(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorConstant(SourceSubtext name) => AssertSpecializedVectorConstant(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupMemberConstant(SourceSubtext name) => AssertVectorGroupMemberConstant(name);

    [Theory]
    [MemberData(nameof(ScalarUnitListAttributesAndUnrecognizedUnitNames))]
    public void ScalarUnitList(string attribute, SourceSubtext name) => AssertScalarUnitList(attribute, name);

    [Theory]
    [MemberData(nameof(ScalarUnitListAttributesAndUnrecognizedUnitNames))]
    public void SpecializedScalarUnitList(string attribute, SourceSubtext name) => AssertSpecializedScalarUnitList(attribute, name);

    [Theory]
    [MemberData(nameof(QuantityUnitListAttributesAndUnrecognizedUnitNames))]
    public void VectorUnitList(string attribute, SourceSubtext name) => AssertVectorUnitList(attribute, name);

    [Theory]
    [MemberData(nameof(QuantityUnitListAttributesAndUnrecognizedUnitNames))]
    public void SpecializedVectorUnitList(string attribute, SourceSubtext name) => AssertSpecializedVectorUnitList(attribute, name);

    [Theory]
    [MemberData(nameof(QuantityUnitListAttributesAndUnrecognizedUnitNames))]
    public void VectorGroupUnitList(string attribute, SourceSubtext name) => AssertVectorGroupUnitList(attribute, name);

    [Theory]
    [MemberData(nameof(QuantityUnitListAttributesAndUnrecognizedUnitNames))]
    public void SpecializedVectorGroupUnitList(string attribute, SourceSubtext name) => AssertSpecializedVectorGroupUnitList(attribute, name);

    public static IEnumerable<object[]> UnrecognizedUnitNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName },
        new object[] { MissingName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");
    private static SourceSubtext MissingName { get; } = SourceSubtext.Covered("\"Metre\"");

    public static IEnumerable<object[]> QuantityUnitListAttributesAndUnrecognizedUnitNames()
    {
        IEnumerable<string> attributes = new[] { "IncludeUnits", "ExcludeUnits" };

        foreach (var attribute in attributes)
        {
            foreach (var unrecognizedName in UnrecognizedUnitNames())
            {
                yield return new object[] { attribute, ParseUnitListUnrecognizedUnitName((SourceSubtext)unrecognizedName.First()) };
            }
        }
    }

    public static IEnumerable<object[]> ScalarUnitListAttributesAndUnrecognizedUnitNames()
    {
        IEnumerable<string> additionalAttributes = new[] { "IncludeBases", "ExcludeBases" };

        foreach (var originalList in QuantityUnitListAttributesAndUnrecognizedUnitNames())
        {
            yield return originalList;
        }

        foreach (var attribute in additionalAttributes)
        {
            foreach (var unrecognizedName in UnrecognizedUnitNames())
            {
                yield return new object[] { attribute, ParseUnitListUnrecognizedUnitName((SourceSubtext)unrecognizedName.First()) };
            }
        }
    }

    private static SourceSubtext ParseUnitListUnrecognizedUnitName(SourceSubtext name)
    {
        if (name.Text is "null")
        {
            return SourceSubtext.Covered("null", prefix: "(string)");
        }

        return name;
    }

    private static GeneratorVerifier AssertExactlyUnrecognizedUnitNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedUnitNameDiagnostics);
    private static IReadOnlyCollection<string> UnrecognizedUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitName };

    private static string UnitAliasText(SourceSubtext aliasOf) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [UnitAlias("Meter", "Meters", {{aliasOf}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
    
    private static GeneratorVerifier AssertUnitAlias(SourceSubtext aliasOf)
    {
        var source = UnitAliasText(aliasOf);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, aliasOf.Context.With(outerPrefix: "UnitAlias(\"Meter\", \"Meters\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string BiasedUnitText(SourceSubtext from) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnit("Celsius", "Celsius", {{from}}, -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiasedUnit(SourceSubtext from)
    {
        var source = BiasedUnitText(from);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, from.Context.With(outerPrefix: "BiasedUnit(\"Celsius\", \"Celsius\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string DerivedUnitText(SourceSubtext signatureElement) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond", new[] { {{signatureElement}}, "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerivedUnit(SourceSubtext signatureElement)
    {
        var source = DerivedUnitText(signatureElement);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, signatureElement.Context.With(outerPrefix: "new[] { ", outerPostfix: ", \"Second\" })]"));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string PrefixedUnitText(SourceSubtext from) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [PrefixedUnit("Kilometre", "Kilometres", {{from}}, MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixedUnit(SourceSubtext from)
    {
        var source = PrefixedUnitText(from);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, from.Context.With(outerPrefix: "PrefixedUnit(\"Kilometre\", \"Kilometres\", ", outerPostfix: ", MetricPrefixName.Kilo)]"));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string ScaledUnitText(SourceSubtext from) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [ScaledUnit("Kilometre", "Kilometres", {{from}}, 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaledUnit(SourceSubtext from)
    {
        var source = ScaledUnitText(from);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, from.Context.With(outerPrefix: "ScaledUnit(\"Kilometre\", \"Kilometres\", ", outerPostfix: ", 1000)]"));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string ScalarDefaultUnitText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarDefaultUnit(SourceSubtext name)
    {
        var source = ScalarDefaultUnitText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarDefaultUnitText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarDefaultUnit(SourceSubtext name)
    {
        var source = SpecializedScalarDefaultUnitText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorDefaultUnitText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDefaultUnit(SourceSubtext name)
    {
        var source = VectorDefaultUnitText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorDefaultUnitText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDefaultUnit(SourceSubtext name)
    {
        var source = SpecializedVectorDefaultUnitText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupDefaultUnitText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupDefaultUnit(SourceSubtext name)
    {
        var source = VectorGroupDefaultUnitText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorGroupDefaultUnitText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroupDefaultUnit(SourceSubtext name)
    {
        var source = SpecializedVectorGroupDefaultUnitText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string ScalarConstantText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        [ScalarConstant("Planck", {{name}}, 1.616255E-35)]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarConstant(SourceSubtext name)
    {
        var source = ScalarConstantText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "\"Planck\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarConstantText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", {{name}}, 1.616255E-35)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarConstant(SourceSubtext name)
    {
        var source = SpecializedScalarConstantText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "\"Planck\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorConstantText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", {{name}}, 1, 1, 1)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorConstant(SourceSubtext name)
    {
        var source = VectorConstantText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "\"MetreOnes\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorConstantText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", {{name}}, 1, 1, 1)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorConstant(SourceSubtext name)
    {
        var source = SpecializedVectorConstantText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "\"MetreOnes\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupMemberConstantText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", {{name}}, 1, 1, 1)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMemberConstant(SourceSubtext name)
    {
        var source = VectorGroupMemberConstantText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "\"MetreOnes\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string ScalarUnitListText(string attribute, SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}({{name}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarUnitList(string attribute, SourceSubtext name)
    {
        var source = ScalarUnitListText(attribute, name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: $"{attribute}("));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarUnitListText(string attribute, SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}({{name}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarUnitList(string attribute, SourceSubtext name)
    {
        var source = SpecializedScalarUnitListText(attribute, name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: $"{attribute}("));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorUnitListText(string attribute, SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{name}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorUnitList(string attribute, SourceSubtext name)
    {
        var source = VectorUnitListText(attribute, name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: $"{attribute}("));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorUnitListText(string attribute, SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{name}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorUnitList(string attribute, SourceSubtext name)
    {
        var source = SpecializedVectorUnitListText(attribute, name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: $"{attribute}("));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupUnitListText(string attribute, SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{name}})]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupUnitList(string attribute, SourceSubtext name)
    {
        var source = VectorGroupUnitListText(attribute, name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: $"{attribute}("));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorGroupUnitListText(string attribute, SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{name}})]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroupUnitList(string attribute, SourceSubtext name)
    {
        var source = SpecializedVectorGroupUnitListText(attribute, name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: $"{attribute}("));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
