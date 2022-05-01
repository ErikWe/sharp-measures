namespace SharpMeasures.Generators.Tests.Diagnostics.SM1000;

using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Units;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitsWithDiagnostics
{
    [Fact]
    public Task MarkedTypeNotPartial()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }
";

        return VerifyGeneratorDiagnostics.VerifyMatch<UnitGenerator>(source);
    }
}
