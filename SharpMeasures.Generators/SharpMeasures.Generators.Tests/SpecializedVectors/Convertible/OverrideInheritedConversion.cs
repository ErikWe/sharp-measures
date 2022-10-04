namespace SharpMeasures.Generators.Tests.SpecializedVectors.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class OverrideInheritedConversion
{
    [Fact]
    public Task Bidirectional() => Verify(BidirectionalText);

    [Fact]
    public Task ModifyOnlyOneDirection() => Verify(ModifyOnlyOneDirectionText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    private static string BidirectionalText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Size3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyOnlyOneDirectionText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Size3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
