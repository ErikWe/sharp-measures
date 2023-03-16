namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Difference
{
    [Fact]
    public Task UnbiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Length.Maths.g.cs");

    [Fact]
    public Task BiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature.Maths.g.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength), Difference = typeof(Distance))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true, Difference = typeof(TemperatureDifference))]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
