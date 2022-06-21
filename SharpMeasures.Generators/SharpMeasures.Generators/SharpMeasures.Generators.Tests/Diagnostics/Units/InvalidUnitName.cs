namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitName
{
    [Fact]
    public Task Alias_Null_ExactListAndVerify()
    {
        string source = Common.Alias(name: "null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Alias_Empty_ExactListAndVerify()
    {
        string source = Common.Alias(name: MetaEmptyString);

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Biased_Null_ExactListAndVerify()
    {
        string source = Common.Biased(name: "null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Biased_Empty_ExactListAndVerify()
    {
        string source = Common.Biased(name: MetaEmptyString);

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Derived_Null_ExactListAndVerify()
    {
        string source = Common.DerivedWithoutSignature(name: "null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Derived_Empty_ExactListAndVerify()
    {
        string source = Common.DerivedWithoutSignature(name: MetaEmptyString);

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Fixed_Null_ExactListAndVerify()
    {
        string source = Common.Fixed(name: "null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Fixed_Empty_ExactListAndVerify()
    {
        string source = Common.Fixed(name: MetaEmptyString);

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Prefixed_Null_ExactListAndVerify()
    {
        string source = Common.Prefixed(name: "null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Prefixed_Empty_ExactListAndVerify()
    {
        string source = Common.Prefixed(name: MetaEmptyString);

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scaled_Null_ExactListAndVerify()
    {
        string source = Common.Scaled(name: "null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scaled_Empty_ExactListAndVerify()
    {
        string source = Common.Scaled(name: MetaEmptyString);

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidUnitNameDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitNameDiagnostics);

    private const string MetaEmptyString = "\"\"";
    private static IReadOnlyCollection<string> InvalidUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitName };
}
