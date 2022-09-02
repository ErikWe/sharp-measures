﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

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
    public void DerivedUnitInstance(SourceSubtext signatureElement) => AssertDerivedUnitInstance(signatureElement);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void PrefixedUnitInstance(SourceSubtext from) => AssertPrefixedUnitInstance(from);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScaledUnit(SourceSubtext from) => AssertScaledUnit(from);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarDefaultUnitInstanceName(SourceSubtext name) => AssertScalarDefaultUnitInstanceName(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarDefaultUnitInstanceName(SourceSubtext name) => AssertSpecializedScalarDefaultUnitInstanceName(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorDefaultUnitInstanceName(SourceSubtext name) => AssertVectorDefaultUnitInstanceName(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorDefaultUnitInstanceName(SourceSubtext name) => AssertSpecializedVectorDefaultUnitInstanceName(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupDefaultUnitInstanceName(SourceSubtext name) => AssertVectorGroupDefaultUnitInstanceName(name);

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorGroupDefaultUnitInstanceName(SourceSubtext name) => AssertSpecializedVectorGroupDefaultUnitInstanceName(name);

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
        IEnumerable<string> additionalAttributes = new[] { "IncludeUnitBases", "ExcludeUnitBases" };

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
    private static IReadOnlyCollection<string> UnrecognizedUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitInstanceName };

    private static string UnitAliasText(SourceSubtext aliasOf) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [UnitInstanceAlias("Meter", "Meters", {{aliasOf}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
    
    private static GeneratorVerifier AssertUnitAlias(SourceSubtext aliasOf)
    {
        var source = UnitAliasText(aliasOf);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, aliasOf.Context.With(outerPrefix: "UnitInstanceAlias(\"Meter\", \"Meters\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedIdentical);
    }

    private static string BiasedUnitText(SourceSubtext from) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnitInstance("Celsius", "Celsius", {{from}}, -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiasedUnit(SourceSubtext from)
    {
        var source = BiasedUnitText(from);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, from.Context.With(outerPrefix: "BiasedUnitInstance(\"Celsius\", \"Celsius\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedIdentical);
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

        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { {{signatureElement}}, "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerivedUnitInstance(SourceSubtext signatureElement)
    {
        var source = DerivedUnitText(signatureElement);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, signatureElement.Context.With(outerPrefix: "new[] { ", outerPostfix: ", \"Second\" })]"));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivedUnitIdentical);
    }

    private static string PrefixedUnitText(SourceSubtext from) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [PrefixedUnitInstance("Kilometre", "Kilometres", {{from}}, MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixedUnitInstance(SourceSubtext from)
    {
        var source = PrefixedUnitText(from);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, from.Context.With(outerPrefix: "PrefixedUnitInstance(\"Kilometre\", \"Kilometres\", ", outerPostfix: ", MetricPrefixName.Kilo)]"));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedIdentical);
    }

    private static string ScaledUnitText(SourceSubtext from) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [ScaledUnitInstance("Kilometre", "Kilometres", {{from}}, 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaledUnit(SourceSubtext from)
    {
        var source = ScaledUnitText(from);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, from.Context.With(outerPrefix: "ScaledUnitInstance(\"Kilometre\", \"Kilometres\", ", outerPostfix: ", 1000)]"));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedIdentical);
    }

    private static string ScalarDefaultUnitInstanceNameText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitInstanceName = {{name}}, DefaultUnitInstanceSymbol = "m")]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarDefaultUnitInstanceName(SourceSubtext name)
    {
        var source = ScalarDefaultUnitInstanceNameText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitInstanceName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedIdentical);
    }

    private static string SpecializedScalarDefaultUnitInstanceNameText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length), DefaultUnitInstanceName = {{name}}, DefaultUnitInstanceSymbol = "m")]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarDefaultUnitInstanceName(SourceSubtext name)
    {
        var source = SpecializedScalarDefaultUnitInstanceNameText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitInstanceName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorDefaultUnitInstanceNameText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), DefaultUnitInstanceName = {{name}}, DefaultUnitInstanceSymbol = "m")]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDefaultUnitInstanceName(SourceSubtext name)
    {
        var source = VectorDefaultUnitInstanceNameText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitInstanceName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorDefaultUnitInstanceNameText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), DefaultUnitInstanceName = {{name}}, DefaultUnitInstanceSymbol = "m")]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDefaultUnitInstanceName(SourceSubtext name)
    {
        var source = SpecializedVectorDefaultUnitInstanceNameText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitInstanceName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupDefaultUnitInstanceNameText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), DefaultUnitInstanceName = {{name}}, DefaultUnitInstanceSymbol = "m")]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupDefaultUnitInstanceName(SourceSubtext name)
    {
        var source = VectorGroupDefaultUnitInstanceNameText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitInstanceName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupDefaultUnitInstanceNameText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position), DefaultUnitInstanceName = {{name}}, DefaultUnitInstanceSymbol = "m")]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroupDefaultUnitInstanceName(SourceSubtext name)
    {
        var source = SpecializedVectorGroupDefaultUnitInstanceNameText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DefaultUnitInstanceName = "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string ScalarConstantText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", {{name}}, 1.616255E-35)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarConstant(SourceSubtext name)
    {
        var source = ScalarConstantText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "\"Planck\", "));

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
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

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static GeneratorVerifier UnbiasedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedIdenticalText);
    private static GeneratorVerifier BiasedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedIdenticalText);
    private static GeneratorVerifier DerivedUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivedUnitIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string UnbiasedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string DerivedUnitIdenticalText => """
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

        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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
