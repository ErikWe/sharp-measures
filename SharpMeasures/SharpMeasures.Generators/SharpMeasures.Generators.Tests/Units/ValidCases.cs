namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ValidCases
{
    [Fact]
    public Task UnbiasedUnit_Verify()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).Verify();
    }
}
