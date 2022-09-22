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
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

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
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [SpecializedSharpMeasuresVectorGroup(typeof(Position), ForwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None, BackwardsCastOperatorBehaviour = ConversionOperatorBehaviour.None)]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
