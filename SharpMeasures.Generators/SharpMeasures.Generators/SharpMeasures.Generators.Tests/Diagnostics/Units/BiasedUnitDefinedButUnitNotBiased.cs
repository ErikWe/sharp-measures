namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class BiasedUnitDefinedButUnitNotBiased
{
    [Fact]
    public Task ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres", 1)]
            [BiasedUnit("Metre2", "Metres2", "Metre", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyBiasedUnitDefinedButUnitNotBiasedDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyBiasedUnitDefinedButUnitNotBiasedDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(BiasedUnitDefinedButUnitNotBiasedDiagnostics);

    private static IReadOnlyCollection<string> BiasedUnitDefinedButUnitNotBiasedDiagnostics { get; } = new string[]
    {
        DiagnosticIDs.BiasedUnitDefinedButUnitNotBiased
    };
}
