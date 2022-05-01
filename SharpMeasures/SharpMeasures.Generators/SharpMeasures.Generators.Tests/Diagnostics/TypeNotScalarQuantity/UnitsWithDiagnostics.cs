namespace SharpMeasures.Generators.Tests.Diagnostics.TypeNotScalarQuantity;

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
    public Task QuantityArgumentNotScalar()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGeneratorDiagnostics.VerifyMatchAndIncludesSpecifiedDiagnostics<UnitGenerator>(source, ExpectedDiagnostics);
    }

    private static IReadOnlyCollection<string> ExpectedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotScalarQuantity };
}
