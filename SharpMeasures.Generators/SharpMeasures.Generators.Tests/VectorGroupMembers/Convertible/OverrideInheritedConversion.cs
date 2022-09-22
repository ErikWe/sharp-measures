namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Convertible;

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

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Position3.Conversions.g.cs");

    private static string BidirectionalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ConvertibleQuantity(typeof(Size), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresVectorGroupMember(typeof(Size))]
        public partial class Size3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Size { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyOnlyOneDirectionText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ConvertibleQuantity(typeof(Size), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVectorGroupMember(typeof(Size))]
        public partial class Size3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Size { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
