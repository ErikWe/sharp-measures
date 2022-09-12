namespace SharpMeasures.Generators.Tests.Units.Definitions.Fixed;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task SimpleFixedUnitInstance() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SimpleFixedUnitInstanceText).VerifyMatchingSourceNames("UnitOfLength.Instances.g.cs");

    private static string SimpleFixedUnitInstanceText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
