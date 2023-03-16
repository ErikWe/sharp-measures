namespace SharpMeasures.Generators.Tests.SpecializedScalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithTwiceOriginalScalar
{
    [Fact]
    public Task Unmodified() => Verify(UnmodifiedText);

    [Fact]
    public Task Reversed() => Verify(ReversedText);

    [Fact]
    public Task ModifyBothDirectionsUsingConvertibleQuantity() => Verify(ModifyBothDirectionsUsingConvertibleQuantityText);

    [Fact]
    public Task ModifyOnlyOneDirectionUsingConvertibleQuantity() => Verify(ModifyOnlyOneDirectionUsingConvertibleQuantityText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Altitude.Conversions.g.cs");

    private static string UnmodifiedText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Distance))]
        public partial class Altitude { }
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ReversedText => """
        using SharpMeasures.Generators;
        
        [SpecializedScalarQuantity(typeof(Distance), ForwardsCastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, BackwardsCastOperatorBehaviour = ConversionOperatorBehaviour.Explicit)]
        public partial class Altitude { }
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyBothDirectionsUsingConvertibleQuantityText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Antidirectional)]
        [SpecializedScalarQuantity(typeof(Distance))]
        public partial class Altitude { }
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyOnlyOneDirectionUsingConvertibleQuantityText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [SpecializedScalarQuantity(typeof(Distance))]
        public partial class Altitude { }

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
