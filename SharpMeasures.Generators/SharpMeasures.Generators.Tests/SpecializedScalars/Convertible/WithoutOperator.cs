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
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ThroughAttributeText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length), ForwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None, BackwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
