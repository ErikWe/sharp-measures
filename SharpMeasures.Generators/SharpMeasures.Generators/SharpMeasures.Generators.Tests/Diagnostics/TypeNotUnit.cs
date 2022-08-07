namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyTypeNotUnitDiagnosticsMessage_Null() => AssertAndVerifyScalarUnit(NullType);

    [Fact]
    public Task VerifyTypeNotUnitDiagnosticsMessage_Int() => AssertAndVerifyScalarUnit(IntType);

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
        new object[] { NullType },
        new object[] { IntType },
        new object[] { LengthType },
        new object[] { PositionType },
        new object[] { DisplacementType },
        new object[] { Position2Type },
        new object[] { Position3Type },
        new object[] { Displacement3Type }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", prefix: "(System.Type)");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int");
    private static SourceSubtext LengthType { get; } = SourceSubtext.AsTypeof("Length");
    private static SourceSubtext PositionType { get; } = SourceSubtext.AsTypeof("Position");
    private static SourceSubtext DisplacementType { get; } = SourceSubtext.AsTypeof("Displacement");
    private static SourceSubtext Position2Type { get; } = SourceSubtext.AsTypeof("Position2");
    private static SourceSubtext Position3Type { get; } = SourceSubtext.AsTypeof("Position3");
    private static SourceSubtext Displacement3Type { get; } = SourceSubtext.AsTypeof("Displacement3");

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

    private static GeneratorVerifier AssertScalarUnit(SourceSubtext unitType)
    {
        var source = ScalarUnitText(unitType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, unitType.Context.With(outerPrefix: "SharpMeasuresScalar("));

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

    private static GeneratorVerifier AssertVectorUnit(SourceSubtext unitType)
    {
        var source = VectorUnitText(unitType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, unitType.Context.With(outerPrefix: "SharpMeasuresVector("));

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

    private static GeneratorVerifier AssertVectorGroupUnit(SourceSubtext unitType)
    {
        var source = VectorGroupUnitText(unitType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, unitType.Context.With(outerPrefix: "SharpMeasuresVectorGroup("));

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

    private static GeneratorVerifier AssertDerivableUnitArgument(SourceSubtext unitType)
    {
        var source = DerivableUnitArgumentText(unitType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, unitType.Context.With(outerPrefix: "DerivableUnit(\"{0}\", "));

        return AssertExactlyTypeNotUnitDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
