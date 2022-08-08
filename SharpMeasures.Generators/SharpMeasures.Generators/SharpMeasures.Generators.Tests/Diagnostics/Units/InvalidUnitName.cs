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
    public void DerivedWithID(SourceSubtext name) => AssertDerived_WithID(name);

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void DerivedWithoutID(SourceSubtext name) => AssertDerived_WithoutID(name);

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
    private static IReadOnlyCollection<string> InvalidUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitName };

    private static GeneratorVerifier AssertAlias(SourceSubtext name)
    {
        var source = SourceTexts.Alias(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "UnitAlias("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertBiased(SourceSubtext name)
    {
        var source = SourceTexts.Biased(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "BiasedUnit("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertDerived_WithID(SourceSubtext name)
    {
        var source = SourceTexts.DerivedWithID(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DerivedUnit("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertDerived_WithoutID(SourceSubtext name)
    {
        var source = SourceTexts.DerivedWithoutID(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "DerivedUnit("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertFixed(SourceSubtext name)
    {
        var source = SourceTexts.Fixed(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "FixedUnit("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertPrefixed(SourceSubtext name)
    {
        var source = SourceTexts.Prefixed(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "PrefixedUnit("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static GeneratorVerifier AssertScaled(SourceSubtext name)
    {
        var source = SourceTexts.Scaled(name: name.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, name.Context.With(outerPrefix: "ScaledUnit("));

        return AssertExactlyInvalidUnitNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
