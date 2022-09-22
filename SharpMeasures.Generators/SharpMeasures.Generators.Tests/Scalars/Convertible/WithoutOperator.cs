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
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature2 { }

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
