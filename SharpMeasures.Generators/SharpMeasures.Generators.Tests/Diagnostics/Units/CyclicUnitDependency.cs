namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class CyclicUnitDependency
{
    [Fact]
    public Task Alias_Self() => AssertAlias_Self().VerifyDiagnostics();

    [Fact]
    public Task UnitAlias_Loop() => AssertAlias_Loop().VerifyDiagnostics();

    [Fact]
    public void BiasedUnit_Self() => AssertBiased_Self();

    [Fact]
    public void BiasedUnit_Loop() => AssertBiased_Loop();

    [Fact]
    public void PrefixedUnit_Self() => AssertPrefixed_Self();

    [Fact]
    public void PrefixedUnit_Loop() => AssertPrefixed_Loop();

    [Fact]
    public void ScaledUnit_Self() => AssertScaled_Self();

    [Fact]
    public void ScaledUnit_Multiple() => AssertScaled_Loop();

    private static GeneratorVerifier AssertExactlyOneCyclicUnitDependencyDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(OneCyclicUnitDependencyDiagnostics);
    private static GeneratorVerifier AssertExactlyTwoCyclicUnitDependencyDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(TwoCyclicUnitDependencyDiagnostics);

    private static IReadOnlyCollection<string> OneCyclicUnitDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.CyclicallyModifiedUnitInstances };
    private static IReadOnlyCollection<string> TwoCyclicUnitDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.CyclicallyModifiedUnitInstances, DiagnosticIDs.CyclicallyModifiedUnitInstances };

    private static string AliasText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [UnitInstanceAlias("Metre", "Metres", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(AliasText_Self, target: "\"Metre\"", prefix: "UnitInstanceAlias(\"Metre\", \"Metres\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(AliasText_Self).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string AliasText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [UnitInstanceAlias("Metre", "Metres", "Meter")]
        [UnitInstanceAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(AliasText_Loop, target: "\"Meter\"", prefix: "UnitInstanceAlias(\"Metre\", \"Metres\", "),
            ExpectedDiagnosticsLocation.TextSpan(AliasText_Loop, target: "\"Metre\"", prefix: "UnitInstanceAlias(\"Meter\", \"Meters\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(AliasText_Loop).AssertDiagnosticsLocation(expectedLocations);
    }

    private static string BiasedText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnitInstance("Kelvin", "Kelvin", "Kelvin", 0)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedText_Self, target: "\"Kelvin\"", prefix: "BiasedUnitInstance(\"Kelvin\", \"Kelvin\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(BiasedText_Self).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string BiasedText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnitInstance("Kelvin", "Kelvin", "Celsius", 273.15)]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(BiasedText_Loop, target: "\"Celsius\"", prefix: "BiasedUnitInstance(\"Kelvin\", \"Kelvin\", "),
            ExpectedDiagnosticsLocation.TextSpan(BiasedText_Loop, target: "\"Kelvin\"", prefix: "BiasedUnitInstance(\"Celsius\", \"Celsius\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(BiasedText_Loop).AssertDiagnosticsLocation(expectedLocations);
    }

    private static string PrefixedText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [PrefixedUnitInstance("Metre", "Metres", "Metre", MetricPrefixName.Identity)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(PrefixedText_Self, target: "\"Metre\"", prefix: "PrefixedUnitInstance(\"Metre\", \"Metres\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(PrefixedText_Self).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string PrefixedText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [PrefixedUnitInstance("Metre", "Metres", "Kilometre", MetricPrefixName.Milli)]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(PrefixedText_Loop, target: "\"Kilometre\"", prefix: "PrefixedUnitInstance(\"Metre\", \"Metres\", "),
            ExpectedDiagnosticsLocation.TextSpan(PrefixedText_Loop, target: "\"Metre\"", prefix: "PrefixedUnitInstance(\"Kilometre\", \"Kilometres\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(PrefixedText_Loop).AssertDiagnosticsLocation(expectedLocations);
    }

    private static string ScaledText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScaledUnitInstance("Metre", "Metres", "Metre", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScaledText_Self, target: "\"Metre\"", prefix: "ScaledUnitInstance(\"Metre\", \"Metres\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(ScaledText_Self).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string ScaledText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScaledUnitInstance("Metre", "Metres", "Kilometre", 0.001)]
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(ScaledText_Loop, target: "\"Kilometre\"", prefix: "ScaledUnitInstance(\"Metre\", \"Metres\", "),
            ExpectedDiagnosticsLocation.TextSpan(ScaledText_Loop, target: "\"Metre\"", prefix: "ScaledUnitInstance(\"Kilometre\", \"Kilometres\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(ScaledText_Loop).AssertDiagnosticsLocation(expectedLocations);
    }
}
