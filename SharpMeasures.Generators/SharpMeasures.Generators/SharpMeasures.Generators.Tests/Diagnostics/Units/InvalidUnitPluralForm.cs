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
    public Task Alias_Null_ExactListAndVerify()
    {
        string source = Common.Alias(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Alias_Empty_ExactListAndVerify()
    {
        string source = Common.Alias(plural: MetaEmptyString);

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Biased_Null_ExactListAndVerify()
    {
        string source = Common.Biased(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Biased_Empty_ExactListAndVerify()
    {
        string source = Common.Biased(plural: MetaEmptyString);

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Derived_Null_ExactListAndVerify()
    {
        string source = Common.DerivedWithoutSignature(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Derived_Empty_ExactListAndVerify()
    {
        string source = Common.DerivedWithoutSignature(plural: MetaEmptyString);

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Fixed_Null_ExactListAndVerify()
    {
        string source = Common.Fixed(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Fixed_Empty_ExactListAndVerify()
    {
        string source = Common.Fixed(plural: MetaEmptyString);

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Prefixed_Null_ExactListAndVerify()
    {
        string source = Common.Prefixed(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Prefixed_Empty_ExactListAndVerify()
    {
        string source = Common.Prefixed(plural: MetaEmptyString);

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scaled_Null_ExactListAndVerify()
    {
        string source = Common.Scaled(plural: "null");

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scaled_Empty_ExactListAndVerify()
    {
        string source = Common.Scaled(plural: MetaEmptyString);

        return AssertExactlyInvalidUnitFormDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidUnitFormDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitFormDiagnostics);

    private const string MetaEmptyString = "\"\"";
    private static IReadOnlyCollection<string> InvalidUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitPluralForm };
}
