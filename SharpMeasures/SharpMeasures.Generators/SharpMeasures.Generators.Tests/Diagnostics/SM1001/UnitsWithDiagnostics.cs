namespace SharpMeasures.Generators.Tests.Diagnostics.SM1001;

using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Units;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitsWithDiagnostics
{
    [Fact]
    public Task QuantityNotScalar()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGeneratorDiagnostics.VerifyMatch<UnitGenerator>(source);
    }
}
