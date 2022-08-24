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
    public void DerivedWithID(SourceSubtext plural) => AssertDerived_WithID(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void DerivedWithoutID(SourceSubtext plural) => AssertDerived_WithoutID(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Fixed_ExactList(SourceSubtext plural) => AssertFixed(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Prefixed_ExactList(SourceSubtext plural) => AssertPrefixed(plural);

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Scaled_ExactList(SourceSubtext plural) => AssertScaled(plural);

    public static IEnumerable<object[]> InvalidUnitPluralForms() => new object[][]
    {
        new object[] { NullPluralForm },
        new object[] { EmptyPluralForm }
    };

    private static SourceSubtext NullPluralForm { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyPluralForm { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidUnitFormDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitFormDiagnostics);
    private static IReadOnlyCollection<string> InvalidUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitPluralForm };

    private static GeneratorVerifier AssertAlias(SourceSubtext plural)
    {
        var source = SourceTexts.Alias(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "UnitAlias(\"Meter\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertBiased(SourceSubtext plural)
    {
        var source = SourceTexts.Biased(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "BiasedUnit(\"Celsius\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertDerived_WithID(SourceSubtext plural)
    {
        var source = SourceTexts.DerivedWithID(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "DerivedUnit(\"MetrePerSecond\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertDerived_WithoutID(SourceSubtext plural)
    {
        var source = SourceTexts.DerivedWithoutID(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "DerivedUnit(\"MetrePerSecond\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertFixed(SourceSubtext plural)
    {
        var source = SourceTexts.Fixed(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "FixedUnit(\"Metre\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertPrefixed(SourceSubtext plural)
    {
        var source = SourceTexts.Prefixed(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "PrefixedUnit(\"Kilometre\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertScaled(SourceSubtext plural)
    {
        var source = SourceTexts.Scaled(plural: plural.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, plural.Context.With(outerPrefix: "ScaledUnit(\"Kilometre\", "));

        return AssertExactlyInvalidUnitFormDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
