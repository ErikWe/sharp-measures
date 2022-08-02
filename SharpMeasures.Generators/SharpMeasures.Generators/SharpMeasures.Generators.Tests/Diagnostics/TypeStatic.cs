namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeStatic
{
    [Fact]
    public Task Unit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(typeof(Length))]
            public static partial class UnitOfLength2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeStaticDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public static partial class Length2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeStaticDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public static partial class Length2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeStaticDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public static partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeStaticDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVector(typeof(Position3))]
            public static partial class Distance3 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeStaticDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroupMember(typeof(Position))]
            public static partial class Position3 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeStaticDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeStaticDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeStaticDiagnostics).AssertAllDiagnosticsValidLocation();

    private static IReadOnlyCollection<string> TypeStaticDiagnostics { get; } = new string[] { DiagnosticIDs.TypeStatic };
}
