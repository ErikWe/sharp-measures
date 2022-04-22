namespace SharpMeasures.Generators.Tests.Diagnostics.SM1000.Units;

using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Units;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithDiagnostics
{
    [Fact]
    public Task PublicReadonlyRecordStruct()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }
";

        return VerifyGeneratorDiagnostics.VerifyMatch<UnitGenerator>(source);
    }
}
