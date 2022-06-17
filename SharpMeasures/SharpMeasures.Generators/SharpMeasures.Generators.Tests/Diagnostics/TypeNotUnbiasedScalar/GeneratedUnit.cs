namespace SharpMeasures.Generators.Tests.Diagnostics.TypeNotUnbiasedScalar;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class SharpMeasuresUnit
{
    [Fact]
    public Task UnbiasedUnit_QuantityArgumentBiased()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[SharpMeasuresUnit(typeof(Temperature))]
public partial class UnitOfTemperature { }
";

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertAllListedDiagnosticsIDsReported(ExpectedDiagnostics).Verify();
    }

    [Fact]
    public Task BiasedUnit_QuantityArgumentBiased()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[SharpMeasuresUnit(typeof(Temperature), AllowBias = true)]
public partial class UnitOfTemperature { }
";

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertAllListedDiagnosticsIDsReported(ExpectedDiagnostics).Verify();
    }

    private static IReadOnlyCollection<string> ExpectedDiagnostics { get; } = new string[] { DiagnosticIDs.ScalarBiased };
}
