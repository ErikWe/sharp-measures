namespace SharpMeasures.Generators.Tests.Diagnostics.TypeNotScalar;

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
    public Task QuantityArgumentNotScalar_VerifyAndDiagnosticsList()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertAllListedDiagnosticsIDsReported(ExpectedDiagnostics).Verify();
    }

    private static IReadOnlyCollection<string> ExpectedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotScalar };
}
