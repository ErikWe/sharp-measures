namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateUnitPluralForm
{
    [Fact]
    public Task Alias() => AssertAlias().VerifyDiagnostics();

    [Fact]
    public void Biased() => AssertBiased();

    [Fact]
    public void Derived() => AssertDerived();

    [Fact]
    public void Prefixed() => AssertPrefixed();

    [Fact]
    public void Scaled() => AssertScaled();

    private static GeneratorVerifier AssertExactlyDuplicateUnitPluralFormDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateUnitFormDiagnostics);
    private static IReadOnlyCollection<string> DuplicateUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateUnitInstancePluralForm };

    private static string AliasText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [UnitInstanceAlias("Meter2", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(AliasText, target: "\"Meters\"", prefix: "UnitInstanceAlias(\"Meter2\", ");

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(AliasText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(AliasIdentical);
    }

    private static string BiasedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [BiasedUnitInstance("Celsius2", "Celsius", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedText, target: "\"Celsius\"", prefix: "BiasedUnitInstance(\"Celsius2\", ");

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(BiasedText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedIdentical);
    }

    private static string Derivedtext => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
        [DerivedUnitInstance("MetrePerSecond2", "MetresPerSecond", new[] { "Metre", "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerived()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(Derivedtext, target: "\"MetresPerSecond\"", prefix: "DerivedUnitInstance(\"MetrePerSecond2\", ");

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(Derivedtext).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivedIdentical);
    }

    private static string PrefixedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [PrefixedUnitInstance("Kilometre2", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(PrefixedText, target: "\"Kilometres\"", prefix: "PrefixedUnitInstance(\"Kilometre2\", ");

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(PrefixedText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(PrefixedIdentical);
    }

    private static string ScaledText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", 1000)]
        [ScaledUnitInstance("Kilometre2", "Kilometres", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScaledText, target: "\"Kilometres\"", prefix: "ScaledUnitInstance(\"Kilometre2\", ");

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(ScaledText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScaledIdentical);
    }

    private static GeneratorVerifier AliasIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(AliasIdenticalText);
    private static GeneratorVerifier BiasedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedIdenticalText);
    private static GeneratorVerifier DerivedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivedIdenticalText);
    private static GeneratorVerifier PrefixedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(PrefixedIdenticalText);
    private static GeneratorVerifier ScaledIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScaledIdenticalText);

    private static string AliasIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string DerivedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string PrefixedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ScaledIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
