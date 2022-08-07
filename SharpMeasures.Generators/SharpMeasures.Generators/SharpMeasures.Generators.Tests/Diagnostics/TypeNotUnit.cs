namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotUnit
{
    [Fact]
    public Task VerifyTypeNotUnitDiagnosticsMessage_Null() => AssertAndVerifyScalarUnit(NullSubtext);

    [Fact]
    public Task VerifyTypeNotUnitDiagnosticsMessage_Int() => AssertAndVerifyScalarUnit(IntSubtext);

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void ScalarUnit(SourceSubtext unitType) => AssertScalarUnit(unitType);

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void VectorUnit(SourceSubtext unitType) => AssertVectorUnit(unitType);

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void VectorGroupUnit(SourceSubtext unitType) => AssertVectorGroupUnit(unitType);

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void DerivableUnitArgument(SourceSubtext unitType) => AssertDerivableUnitArgument(unitType);

    private static IEnumerable<object[]> NonUnitTypes() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { IntSubtext },
        new object[] { LengthSubtext },
        new object[] { PositionSubtext },
        new object[] { DisplacementSubtext },
        new object[] { Position2Subtext },
        new object[] { Position3Subtext },
        new object[] { Displacement3Subtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null");
    private static SourceSubtext IntSubtext { get; } = SourceSubtext.Typeof("int");
    private static SourceSubtext LengthSubtext { get; } = SourceSubtext.Typeof("Length");
    private static SourceSubtext PositionSubtext { get; } = SourceSubtext.Typeof("Position");
    private static SourceSubtext DisplacementSubtext { get; } = SourceSubtext.Typeof("Displacement");
    private static SourceSubtext Position2Subtext { get; } = SourceSubtext.Typeof("Position2");
    private static SourceSubtext Position3Subtext { get; } = SourceSubtext.Typeof("Position3");
    private static SourceSubtext Displacement3Subtext { get; } = SourceSubtext.Typeof("Displacement3");

    private static GeneratorVerifier AssertExactlyTypeNotUnitDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotUnitDiagnostics);
    private static IReadOnlyCollection<string> TypeNotUnitDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnit };

    private static string ScalarUnitText(SourceSubtext unitType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar({{unitType}})]
        public partial class Distance { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ScalarUnitLocation(SourceSubtext unitType) => ExpectedDiagnosticsLocation.TextSpan(ScalarUnitText(unitType), unitType, prefix: "SharpMeasuresScalar(");

    private static GeneratorVerifier AssertScalarUnit(SourceSubtext unitType)
    {
        var source = ScalarUnitText(unitType);
        var expectedLocation = ScalarUnitLocation(unitType);

        return AssertExactlyTypeNotUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyScalarUnit(SourceSubtext unitType) => AssertScalarUnit(unitType).VerifyDiagnostics();

    private static string VectorUnitText(SourceSubtext unitType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector({{unitType}})]
        public partial class Position4 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorUnitLocation(SourceSubtext unitType) => ExpectedDiagnosticsLocation.TextSpan(VectorUnitText(unitType), unitType, prefix: "SharpMeasuresVector(");

    private static GeneratorVerifier AssertVectorUnit(SourceSubtext unitType)
    {
        var source = VectorUnitText(unitType);
        var expectedLocation = VectorUnitLocation(unitType);

        return AssertExactlyTypeNotUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupUnitText(SourceSubtext unitType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup({{unitType}})]
        public static partial class Size { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorGroupUnitLocation(SourceSubtext unitType) => ExpectedDiagnosticsLocation.TextSpan(VectorGroupUnitText(unitType), unitType, prefix: "SharpMeasuresVectorGroup(");

    private static GeneratorVerifier AssertVectorGroupUnit(SourceSubtext unitType)
    {
        var source = VectorGroupUnitText(unitType);
        var expectedLocation = VectorGroupUnitLocation(unitType);

        return AssertExactlyTypeNotUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string DerivableUnitArgumentText(SourceSubtext unitType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit("{0}", {{unitType}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        """;

    private static TextSpan DerivableUnitArgumentLocation(SourceSubtext unitType) => ExpectedDiagnosticsLocation.TextSpan(DerivableUnitArgumentText(unitType), unitType, prefix: "DerivableUnit(\"{0}\", ");

    private static GeneratorVerifier AssertDerivableUnitArgument(SourceSubtext unitType)
    {
        if (unitType.Target is "null")
        {
            unitType = unitType with { Prefix = $"{unitType.Prefix}(System.Type)" };
        }

        var source = DerivableUnitArgumentText(unitType);
        var expectedLocation = DerivableUnitArgumentLocation(unitType);

        return AssertExactlyTypeNotUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
