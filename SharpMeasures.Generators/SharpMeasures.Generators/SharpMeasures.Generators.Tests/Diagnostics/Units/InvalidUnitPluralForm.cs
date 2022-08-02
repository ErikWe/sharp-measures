namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitPluralForm
{
    [Fact]
    public Task VerifyInvalidUniPluralFormDiagnosticsMessage_Null()
    {
        string source = SourceTexts.Fixed(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyInvalidUnitPluralFormDiagnosticsMessage_Empty()
    {
        string source = SourceTexts.Fixed(plural: "\"\"");

        return AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Alias_ExactList(string plural)
    {
        string source = SourceTexts.Alias(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Biased_ExactList(string plural)
    {
        string source = SourceTexts.Biased(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void DerivedWithID_ExactList(string plural)
    {
        string source = SourceTexts.DerivedWithID(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void DerivedWithoutID_ExactList(string plural)
    {
        string source = SourceTexts.DerivedWithoutID(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Fixed_ExactList(string plural)
    {
        string source = SourceTexts.Fixed(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Prefixed_ExactList(string plural)
    {
        string source = SourceTexts.Prefixed(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitPluralForms))]
    public void Scaled_ExactList(string plural)
    {
        string source = SourceTexts.Scaled(plural: plural);

        AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> InvalidUnitPluralForms() => new object[][]
    {
        new[] { "null" },
        new[] { "\"\"" }
    };

    private static GeneratorVerifier AssertExactlyInvalidUnitFormDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitFormDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> InvalidUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitPluralForm };
}
