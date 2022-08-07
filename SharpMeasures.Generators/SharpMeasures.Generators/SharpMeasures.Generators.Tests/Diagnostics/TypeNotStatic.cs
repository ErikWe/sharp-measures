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
    public Task VectorGroup() => AssertAndVerifyVectorGroup();

    [Fact]
    public Task SpecializedVectorGroup() => AssertAndVerifySpecializedVectorGroup();

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

    private static TextSpan VectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "Position", prefix: "public partial class ");

    private static GeneratorVerifier AssertVectorGroup() => AssertExactlyTypeNotStaticDiagnostics(VectorGroupText).AssertDiagnosticsLocation(VectorGroupLocation, VectorGroupText);
    private static Task AssertAndVerifyVectorGroup() => AssertVectorGroup().VerifyDiagnostics();

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

    private static TextSpan SpecializedVectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "Displacement", prefix: "public partial class ");

    private static GeneratorVerifier AssertSpecializedVectorGroup() => AssertExactlyTypeNotStaticDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(SpecializedVectorGroupLocation, SpecializedVectorGroupText);
    private static Task AssertAndVerifySpecializedVectorGroup() => AssertSpecializedVectorGroup().VerifyDiagnostics();
}
