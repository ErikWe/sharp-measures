namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithoutOperator
{
    [Fact]
    public Task ThroughConvertibleQuantity() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ThroughConvertibleQuantityText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    [Fact]
    public void ThroughAttribute() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ThroughAttributeText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(ThroughConvertibleQuantityText);

    private static string ThroughConvertibleQuantityText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ThroughAttributeText => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [SpecializedVectorGroup(typeof(Position), ForwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None, BackwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        public static partial class Displacement { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
