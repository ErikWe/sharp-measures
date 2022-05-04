namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ValidCases
{
    [Fact]
    public Task Unbiased()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        return GeneratorVerifier.Construct<UnitGenerator>(source).Verify();
    }
}
