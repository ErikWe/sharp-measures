namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotStatic
{
    [Fact]
    public Task VectorGroup_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotStaticDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVectorGroup_ExactList()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
            public partial class Distance { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotStaticDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotStaticDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotStaticDiagnostics);

    private static IReadOnlyCollection<string> TypeNotStaticDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotStatic };
}
