namespace SharpMeasures.Generators.Tests.Scalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Antidirectional
{
    [Fact]
    public void UnbiasedScalar_Default() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Explicit"));

    [Fact]
    public Task UnbiasedScalar_Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Implicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    [Fact]
    public Task UnbiasedScalar_Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Explicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    [Fact]
    public void BiasedScalar_Default() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Explicit"));

    [Fact]
    public Task BiasedScalar_Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Implicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature2.Conversions.g.cs");

    [Fact]
    public Task BiasedScalar_Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Explicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature2.Conversions.g.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Antidirectional)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string UnbiasedScalarText_SpecifiedBehaviour(string behaviour) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Antidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
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

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Antidirectional)]
        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature2 { }

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string BiasedScalarText_SpecifiedBehaviour(string behaviour) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Antidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
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
