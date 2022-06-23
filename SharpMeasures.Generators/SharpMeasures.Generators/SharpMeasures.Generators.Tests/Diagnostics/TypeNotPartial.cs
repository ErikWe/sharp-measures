namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotPartial
{
    [Fact]
    public Task Unit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(typeof(int))]
            public class UnitOfLength2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotPartialDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public class Length2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotPartialDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotPartialDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public class Position2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotPartialDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotPartialDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotPartialDiagnostics);

    private static IReadOnlyCollection<string> TypeNotPartialDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotPartial };
}
