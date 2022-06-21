namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class FixedUnitBiasSpecifiedButUnitNotBiased
{
    [Fact]
    public Task NonZero_ExactListAndVerify()
    {
        string source = Common.Fixed(bias: "1");

        return AssertExactlyFixedUnitBiasSpecifiedButUnitNotBiasedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public void Zero_NoDiagnostics()
    {
        string source = Common.Fixed(bias: "0");

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported();
    }

    private static GeneratorVerifier AssertExactlyFixedUnitBiasSpecifiedButUnitNotBiasedDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(FixedUnitBiasSpecifiedButUnitNotBiasedDiagnostics);

    private static IReadOnlyCollection<string> FixedUnitBiasSpecifiedButUnitNotBiasedDiagnostics { get; } = new string[]
    {
        DiagnosticIDs.FixedUnitBiasSpecifiedButUnitNotBiased
    };
}
