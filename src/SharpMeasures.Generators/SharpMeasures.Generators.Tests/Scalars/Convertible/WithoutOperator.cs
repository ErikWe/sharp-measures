namespace SharpMeasures.Generators.Tests.Scalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithoutOperator
{
    [Fact]
    public Task UnbiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    [Fact]
    public Task BiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature2.Conversions.g.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature2 { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
