namespace SharpMeasures.Generators.Tests.SpecializedScalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithoutOperator
{
    [Fact]
    public Task ThroughConvertibleQuantity() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ThroughConvertibleQuantityText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    [Fact]
    public void ThroughAttribute() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ThroughAttributeText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(ThroughConvertibleQuantityText);

    private static string ThroughConvertibleQuantityText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ThroughAttributeText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length), ForwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None, BackwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
