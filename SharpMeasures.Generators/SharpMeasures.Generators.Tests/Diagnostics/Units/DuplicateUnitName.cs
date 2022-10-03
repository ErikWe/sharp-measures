namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

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
    private static IReadOnlyCollection<string> DuplicateUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateUnitInstanceName };

    private static string AliasText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [UnitInstanceAlias("Meter", "Meters2", "Metre")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(AliasText, "\"Meter\"", postfix: ", \"Meters2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(AliasText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(AliasIdentical);
    }

    private static string BiasedText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [BiasedUnitInstance("Celsius", "Celsius2", "Kelvin", -273.15)]
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedText, "\"Celsius\"", postfix: ", \"Celsius2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(BiasedText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedIdentical);
    }

    private static string DerivedText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnitInstance("Second", "Seconds")]
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond2", new[] { "Metre", "Second" })]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerived()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(DerivedText, "\"MetrePerSecond\"", postfix: ", \"MetresPerSecond2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(DerivedText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivedIdentical);
    }

    private static string PrefixedText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [PrefixedUnitInstance("Kilometre", "Kilometres2", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(PrefixedText, "\"Kilometre\"", postfix: ", \"Kilometres2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(PrefixedText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(PrefixedIdentical);
    }

    private static string ScaledText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", 1000)]
        [ScaledUnitInstance("Kilometre", "Kilometres2", "Metre", 1000)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScaledText, "\"Kilometre\"", postfix: ", \"Kilometres2\"");

        return AssertExactlyDuplicateUnitNameDiagnostics(ScaledText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScaledIdentical);
    }

    private static GeneratorVerifier AliasIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(AliasIdenticalText);
    private static GeneratorVerifier BiasedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedIdenticalText);
    private static GeneratorVerifier DerivedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivedIdenticalText);
    private static GeneratorVerifier PrefixedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(PrefixedIdenticalText);
    private static GeneratorVerifier ScaledIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScaledIdenticalText);

    private static string AliasIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string DerivedIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnitInstance("Second", "Seconds")]
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string PrefixedIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ScaledIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", 1000)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
