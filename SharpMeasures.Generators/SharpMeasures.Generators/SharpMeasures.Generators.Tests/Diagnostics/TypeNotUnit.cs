namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotUnit
{
    [Fact]
    public Task VerifyTypeNotUnitDiagnosticsMessage_Null()
    {
        string source = ScalarUnitText("null");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotUnitDiagnosticsMessage_Int()
    {
        string source = ScalarUnitText("typeof(int)");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void ScalarUnit_ExactList(string value)
    {
        string source = ScalarUnitText(value);

        AssertExactlyTypeNotUnitDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void VectorUnit_ExactList(string value)
    {
        string source = VectorUnitText(value);

        AssertExactlyTypeNotUnitDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonUnitTypes))]
    public void DerivableUnitArgument_ExactList(string value)
    {
        string source = VectorUnitText(value);

        AssertExactlyTypeNotUnitDiagnostics(source);
    }

    private static IEnumerable<object[]> NonUnitTypes() => new object[][]
    {
        new[] { "null" },
        new[] { "typeof(int)" },
        new[] { "typeof(Length)" },
        new[] { "typeof(Position3)" },
        new[] { "typeof(Displacement3)" },
        new[] { "typeof(Position)" },
        new[] { "typeof(Displacement)" },
        new[] { "typeof(Position2)" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotUnitDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotUnitDiagnostics);
    private static IReadOnlyCollection<string> TypeNotUnitDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnit };

    private static string ScalarUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar({{value}})]
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

    private static string VectorUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector({{value}})]
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

    private static string DerivableUnitArgumentText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit("_", "{0}", {{value}})]
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
}
