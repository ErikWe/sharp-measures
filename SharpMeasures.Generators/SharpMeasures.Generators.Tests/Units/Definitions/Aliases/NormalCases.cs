namespace SharpMeasures.Generators.Tests.Units.Definitions.Aliases;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task SimpleUnitAlias()
    {
        var source = """
            using SharpMeasures.Generators;
            
            [ScalarQuantity(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnitInstance("Metre", "Metres")]
            [UnitInstanceAlias("Meter", "Meters", "Metre")]
            [Unit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfLength.Instances.g.cs");
    }
}
