namespace SharpMeasures.Generators.Tests.Diagnostics.TypeNotUnbiasedScalarQuantity;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitsWithDiagnostics
{
    [Fact]
    public Task UnbiasedUnit_QuantityArgumentBiased()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature))]
public partial class UnitOfTemperature { }
";

        return GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics).Verify();
    }

    [Fact]
    public Task BiasedUnit_QuantityArgumentBiased()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature), AllowBias = true)]
public partial class UnitOfTemperature { }
";

        return GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics).Verify();
    }

    private static IReadOnlyCollection<string> ExpectedDiagnostics { get; } = new string[] { DiagnosticIDs.ScalarNotUnbiased };
}
