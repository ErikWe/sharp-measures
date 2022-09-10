namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task UnbiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).VerifyMatchingSourceNames(@"^Length.\S+\.g\.cs");

    [Fact]
    public Task BiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).VerifyMatchingSourceNames(@"^Temperature.\S+\.g\.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
