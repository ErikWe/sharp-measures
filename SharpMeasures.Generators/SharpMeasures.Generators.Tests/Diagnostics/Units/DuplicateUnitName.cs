﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateUnitName
{
    [Fact]
    public Task Alias() => AssertAlias().VerifyDiagnostics();

    [Fact]
    public void Biased() => AssertBiased();

    [Fact]
    public void DerivedUnit_ExactList() => AssertDerived();

    [Fact]
    public void PrefixedUnit_ExactList() => AssertPrefixed();

    [Fact]
    public void ScaledUnit_ExactList() => AssertScaled();

    private static GeneratorVerifier AssertExactlyDuplicateUnitNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateUnitNameDiagnostics);
    private static IReadOnlyCollection<string> DuplicateUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateUnitName };

    private static string AliasText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [UnitAlias("Meter", "Meters2", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(AliasText, "\"Meter\"", postfix: ", \"Meters2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(AliasText).AssertDiagnosticsLocation(expectedLocation, AliasText);
    }

    private static string BiasedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnit("Kelvin", "Kelvin")]
        [BiasedUnit("Celsius", "Celsius", "Kelvin", -273.15)]
        [BiasedUnit("Celsius", "Celsius2", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedText, "\"Celsius\"", postfix: ", \"Celsius2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(BiasedText).AssertDiagnosticsLocation(expectedLocation, BiasedText);
    }

    private static string DerivedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond2", new[] { "Metre", "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerived()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(DerivedText, "\"MetrePerSecond\"", postfix: ", \"MetresPerSecond2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(DerivedText).AssertDiagnosticsLocation(expectedLocation, DerivedText);
    }

    private static string PrefixedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [PrefixedUnit("Kilometre", "Kilometres2", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(PrefixedText, "\"Kilometre\"", postfix: ", \"Kilometres2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(PrefixedText).AssertDiagnosticsLocation(expectedLocation, PrefixedText);
    }

    private static string ScaledText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [ScaledUnit("Kilometre", "Kilometres", "Metre", 1000)]
        [ScaledUnit("Kilometre", "Kilometres2", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScaledText, "\"Kilometre\"", postfix: ", \"Kilometres2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(ScaledText).AssertDiagnosticsLocation(expectedLocation, ScaledText);
    }
}