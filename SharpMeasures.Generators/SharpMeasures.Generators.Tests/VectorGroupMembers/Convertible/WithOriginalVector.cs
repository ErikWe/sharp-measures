namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithOriginalVector
{
    [Fact]
    public Task Unmodified() => Verify(UnmodifiedText);

    [Fact]
    public Task Reversed() => Verify(ReversedText);

    [Fact]
    public Task ModifyBothDirectionsUsingConvertibleQuantity() => Verify(ModifyBothDirectionsUsingConvertibleQuantityText);

    [Fact]
    public Task ModifyOnlyOneDirectionUsingConvertibleQuantity() => Verify(ModifyOnlyOneDirectionUsingConvertibleQuantityText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    private static string UnmodifiedText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
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

    private static string ReversedText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [SpecializedSharpMeasuresVectorGroup(typeof(Position), ForwardsCastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, BackwardsCastOperatorBehaviour = ConversionOperatorBehaviour.Explicit)]
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

    private static string ModifyBothDirectionsUsingConvertibleQuantityText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position3), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [ConvertibleQuantity(typeof(Position3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Antidirectional)]
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

    private static string ModifyOnlyOneDirectionUsingConvertibleQuantityText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position3), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
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
}
