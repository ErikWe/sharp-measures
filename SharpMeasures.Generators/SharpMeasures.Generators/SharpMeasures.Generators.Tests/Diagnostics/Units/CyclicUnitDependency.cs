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

    private static GeneratorVerifier AssertExactlyOneCyclicUnitDependencyDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(OneCyclicUnitDependencyDiagnostics);
    private static GeneratorVerifier AssertExactlyTwoCyclicUnitDependencyDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TwoCyclicUnitDependencyDiagnostics);

    private static IReadOnlyCollection<string> OneCyclicUnitDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.CyclicUnitDependency };
    private static IReadOnlyCollection<string> TwoCyclicUnitDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.CyclicUnitDependency, DiagnosticIDs.CyclicUnitDependency };

    private static string AliasText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [UnitAlias("Metre", "Metres", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(AliasText_Self, target: "\"Metre\"", prefix: "UnitAlias(\"Metre\", \"Metres\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(AliasText_Self).AssertDiagnosticsLocation(expectedLocation, AliasText_Self);
    }

    private static string AliasText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [UnitAlias("Metre", "Metres", "Meter")]
        [UnitAlias("Meter", "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(AliasText_Loop, target: "\"Meter\"", prefix: "UnitAlias(\"Metre\", \"Metres\", "),
            ExpectedDiagnosticsLocation.TextSpan(AliasText_Loop, target: "\"Metre\"", prefix: "UnitAlias(\"Meter\", \"Meters\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(AliasText_Loop).AssertDiagnosticsLocation(expectedLocations, AliasText_Loop);
    }

    private static string BiasedText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnit("Kelvin", "Kelvin", "Kelvin", 0)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedText_Self, target: "\"Kelvin\"", prefix: "BiasedUnit(\"Kelvin\", \"Kelvin\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(BiasedText_Self).AssertDiagnosticsLocation(expectedLocation, BiasedText_Self);
    }

    private static string BiasedText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnit("Kelvin", "Kelvin", "Celsius", 273.15)]
        [BiasedUnit("Celsius", "Celsius", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(BiasedText_Loop, target: "\"Celsius\"", prefix: "BiasedUnit(\"Kelvin\", \"Kelvin\", "),
            ExpectedDiagnosticsLocation.TextSpan(BiasedText_Loop, target: "\"Kelvin\"", prefix: "BiasedUnit(\"Celsius\", \"Celsius\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(BiasedText_Loop).AssertDiagnosticsLocation(expectedLocations, BiasedText_Loop);
    }

    private static string PrefixedText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [PrefixedUnit("Metre", "Metres", "Metre", MetricPrefixName.Identity)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(PrefixedText_Self, target: "\"Metre\"", prefix: "PrefixedUnit(\"Metre\", \"Metres\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(PrefixedText_Self).AssertDiagnosticsLocation(expectedLocation, PrefixedText_Self);
    }

    private static string PrefixedText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [PrefixedUnit("Metre", "Metres", "Kilometre", MetricPrefixName.Milli)]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(PrefixedText_Loop, target: "\"Kilometre\"", prefix: "PrefixedUnit(\"Metre\", \"Metres\", "),
            ExpectedDiagnosticsLocation.TextSpan(PrefixedText_Loop, target: "\"Metre\"", prefix: "PrefixedUnit(\"Kilometre\", \"Kilometres\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(PrefixedText_Loop).AssertDiagnosticsLocation(expectedLocations, PrefixedText_Loop);
    }

    private static string ScaledText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScaledUnit("Metre", "Metres", "Metre", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScaledText_Self, target: "\"Metre\"", prefix: "ScaledUnit(\"Metre\", \"Metres\", ");

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(ScaledText_Self).AssertDiagnosticsLocation(expectedLocation, ScaledText_Self);
    }

    private static string ScaledText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScaledUnit("Metre", "Metres", "Kilometre", 0.001)]
        [ScaledUnit("Kilometre", "Kilometres", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(ScaledText_Loop, target: "\"Kilometre\"", prefix: "ScaledUnit(\"Metre\", \"Metres\", "),
            ExpectedDiagnosticsLocation.TextSpan(ScaledText_Loop, target: "\"Metre\"", prefix: "ScaledUnit(\"Kilometre\", \"Kilometres\", ")
        };

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(ScaledText_Loop).AssertDiagnosticsLocation(expectedLocations, ScaledText_Loop);
    }
}
