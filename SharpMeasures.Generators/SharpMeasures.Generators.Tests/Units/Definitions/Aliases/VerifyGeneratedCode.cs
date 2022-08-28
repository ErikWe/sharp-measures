namespace SharpMeasures.Generators.Tests.Units.Definitions.Aliases;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VerifyGeneratedCode
{
    [Fact]
    public Task SimpleUnitAlias()
    {
        var source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [UnitAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames("UnitOfLength_Definitions.g.cs");
    }
}
