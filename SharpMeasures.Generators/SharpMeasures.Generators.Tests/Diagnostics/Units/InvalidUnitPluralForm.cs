namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitPluralForm
{
    [Fact]
    public Task VerifyInvalidUniPluralFormDiagnosticsMessage_Null() => AssertFixed(NullPluralForm).VerifyDiagnostics();

    [Fact]
    public Task VerifyInvalidUnitPluralFormDiagnosticsMessage_Empty() => AssertFixed(EmptyPluralForm).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Alias(SourceSubtext plural) => AssertAlias(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Biased(SourceSubtext plural) => AssertBiased(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Derived(SourceSubtext plural) => AssertDerived(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Fixed(SourceSubtext plural) => AssertFixed(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Prefixed(SourceSubtext plural) => AssertPrefixed(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Scaled(SourceSubtext plural) => AssertScaled(plural);

    public static IEnumerable<object[]> InvalidUnitPluralForms() => new object[][]
    {
        new object[] { NullPluralForm },
        new object[] { EmptyPluralForm }
    };

    private static SourceSubtext NullPluralForm { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyPluralForm { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidUnitFormDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitFormDiagnostics);
    private static IReadOnlyCollection<string> InvalidUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitInstancePluralForm };

    private static string AliasText(SourceSubtext plural) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [UnitInstanceAlias("Meter", {{plural}}, "Metre")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertAlias(SourceSubtext plural)
    {
        var source = AliasText(plural);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "UnitInstanceAlias(\"Meter\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(NonDerivableIdentical);
    }

    private static string BiasedText(SourceSubtext plural) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", {{plural}}, "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased(SourceSubtext plural)
    {
        var source = BiasedText(plural);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "BiasedUnitInstance(\"Celsius\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedIdentical);
    }

    private static string DerivedText(SourceSubtext plural) => $$"""
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
        [DerivedUnitInstance("MetrePerSecond", {{plural}}, new[] { "Metre", "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerived(SourceSubtext plural)
    {
        var source = DerivedText(plural);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "DerivedUnitInstance(\"MetrePerSecond\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivableIdentical);
    }

    private static string FixedText(SourceSubtext plural) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", {{plural}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertFixed(SourceSubtext plural)
    {
        var source = FixedText(plural);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "FixedUnitInstance(\"Metre\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(FixedIdentical);
    }

    private static string PrefixedText(SourceSubtext plural) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", {{plural}}, "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixed(SourceSubtext plural)
    {
        var source = PrefixedText(plural);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "PrefixedUnitInstance(\"Kilometre\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(NonDerivableIdentical);
    }

    private static string ScaledText(SourceSubtext plural) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometre", {{plural}}, "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScaled(SourceSubtext plural)
    {
        var source = ScaledText(plural);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "ScaledUnitInstance(\"Kilometre\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(NonDerivableIdentical);
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
