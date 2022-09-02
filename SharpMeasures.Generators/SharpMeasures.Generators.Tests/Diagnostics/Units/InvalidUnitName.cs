namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitName
{
    [Fact]
    public Task VerifyInvalidUnitNameDiagnosticsMessage_Null() => AssertFixed(NullName).VerifyDiagnostics();

    [Fact]
    public Task VerifyInvalidUnitNameDiagnosticsMessage_Empty() => AssertFixed(EmptyName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Alias(SourceSubtext name) => AssertAlias(name);

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Biased(SourceSubtext name) => AssertBiased(name);

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Derived(SourceSubtext name) => AssertDerived(name);

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Fixed(SourceSubtext name) => AssertFixed(name);

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Prefixed(SourceSubtext name) => AssertPrefixed(name);

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Scaled(SourceSubtext name) => AssertScaled(name);

    public static IEnumerable<object[]> InvalidUnitNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidUnitNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitInstanceName };

    private static string AliasText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias({{name}}, "Meters", "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias(SourceSubtext name)
    {
        var source = AliasText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "UnitInstanceAlias("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(NonDerivableIdentical);
    }

    private static string BiasedText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance({{name}}, "Celsius", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased(SourceSubtext name)
    {
        var source = BiasedText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "BiasedUnitInstance("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedIdentical);
    }

    private static string DerivedText(SourceSubtext name) => $$"""
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
        [DerivedUnitInstance({{name}}, "MetresPerSecond", new[] { "Metre", "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerived(SourceSubtext name)
    {
        var source = DerivedText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DerivedUnitInstance("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivableIdentical);
    }

    private static string FixedText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance({{name}}, "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertFixed(SourceSubtext name)
    {
        var source = FixedText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "FixedUnitInstance("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(FixedIdentical);
    }

    private static string PrefixedText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance({{name}}, "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed(SourceSubtext name)
    {
        var source = PrefixedText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "PrefixedUnitInstance("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(NonDerivableIdentical);
    }

    private static string ScaledText(SourceSubtext name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance({{name}}, "Kilometres", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled(SourceSubtext name)
    {
        var source = ScaledText(name);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "ScaledUnitInstance("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(NonDerivableIdentical);
    }

    private static GeneratorVerifier NonDerivableIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(NonDerivableIdenticalText);
    private static GeneratorVerifier BiasedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedIdenticalText);
    private static GeneratorVerifier DerivableIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivableIdenticalText);
    private static GeneratorVerifier FixedIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(FixedIdenticalText);

    private static string NonDerivableIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string DerivableIdenticalText => """
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
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string FixedIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
