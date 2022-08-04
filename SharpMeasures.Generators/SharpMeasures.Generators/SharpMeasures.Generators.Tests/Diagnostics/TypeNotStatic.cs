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
public class TypeNotStatic
{
    [Fact]
    public Task VectorGroup()
    {
        return AssertExactlyTypeNotStaticDiagnostics(VectorGroupText).AssertDiagnosticsLocation(VectorGroupLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVectorGroup()
    {
        return AssertExactlyTypeNotStaticDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(SpecializedVectorGroupLocation).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotStaticDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotStaticDiagnostics);
    private static IReadOnlyCollection<string> TypeNotStaticDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotStatic };

    private static string VectorGroupText => """
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

    private static IEnumerable<TextSpan> VectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, "Position", "public partial class ");

    private static string SpecializedVectorGroupText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedVectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, "Displacement", "public partial class ");
}
