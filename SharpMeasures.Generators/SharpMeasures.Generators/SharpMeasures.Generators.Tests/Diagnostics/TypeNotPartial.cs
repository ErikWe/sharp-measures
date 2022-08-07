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
public class TypeNotPartial
{
    [Fact]
    public Task Unit() => AssertAndVerifyUnit();

    [Fact]
    public Task Scalar() => AssertAndVerifyScalar();

    [Fact]
    public Task SpecializedScalar() => AssertAndVerifySpecializedScalar();

    [Fact]
    public Task Vector() => AssertAndVerifyVector();

    [Fact]
    public Task SpecializedVector() => AssertAndVerifySpecializedVector();

    [Fact]
    public Task VectorGroup() => AssertAndVerifyVectorGroup();

    [Fact]
    public Task SpecializedVectorGroupy() => AssertAndVerifySpecializedVectorGroup();

    [Fact]
    public Task VectorGroupMember() => AssertAndVerifyVectorGroupMember();

    private static GeneratorVerifier AssertExactlyTypeNotPartialDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotPartialDiagnostics);
    private static IReadOnlyCollection<string> TypeNotPartialDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotPartial };

    private static string UnitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresUnit(typeof(Length))]
        public class UnitOfLength2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan UnitLocation => ExpectedDiagnosticsLocation.TextSpan(UnitText, target: "UnitOfLength2", prefix: "public class ");

    private static GeneratorVerifier AssertUnit() => AssertExactlyTypeNotPartialDiagnostics(UnitText).AssertDiagnosticsLocation(UnitLocation, UnitText);
    private static Task AssertAndVerifyUnit() => AssertUnit().VerifyDiagnostics();

    private static string ScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public class Length2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ScalarLocation => ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "Length2", prefix: "public class ");

    private static GeneratorVerifier AssertScalar() => AssertExactlyTypeNotPartialDiagnostics(ScalarText).AssertDiagnosticsLocation(ScalarLocation, ScalarText);
    private static Task AssertAndVerifyScalar() => AssertScalar().VerifyDiagnostics();

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public class Length2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedScalarLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "Length2", prefix: "public class ");

    private static GeneratorVerifier AssertSpecializedScalar() => AssertExactlyTypeNotPartialDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(SpecializedScalarLocation, SpecializedScalarText);
    private static Task AssertAndVerifySpecializedScalar() => AssertSpecializedScalar().VerifyDiagnostics();

    private static string VectorText => """
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

    private static TextSpan VectorLocation => ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "Position3", prefix: "public class ");

    private static GeneratorVerifier AssertVector() => AssertExactlyTypeNotPartialDiagnostics(VectorText).AssertDiagnosticsLocation(VectorLocation, VectorText);
    private static Task AssertAndVerifyVector() => AssertVector().VerifyDiagnostics();

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedVectorLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "Displacement3", prefix: "public class ");

    private static GeneratorVerifier AssertSpecializedVector() => AssertExactlyTypeNotPartialDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(SpecializedVectorLocation, SpecializedVectorText);
    private static Task AssertAndVerifySpecializedVector() => AssertSpecializedVector().VerifyDiagnostics();

    private static string VectorGroupText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "Position", prefix: "public static class ");

    private static GeneratorVerifier AssertVectorGroup() => AssertExactlyTypeNotPartialDiagnostics(VectorGroupText).AssertDiagnosticsLocation(VectorGroupLocation, VectorGroupText);
    private static Task AssertAndVerifyVectorGroup() => AssertVectorGroup().VerifyDiagnostics();

    private static string SpecializedVectorGroupText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedVectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "Displacement", prefix: "public static class ");

    private static GeneratorVerifier AssertSpecializedVectorGroup() => AssertExactlyTypeNotPartialDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(SpecializedVectorGroupLocation, SpecializedVectorGroupText);
    private static Task AssertAndVerifySpecializedVectorGroup() => AssertSpecializedVectorGroup().VerifyDiagnostics();

    private static string VectorGroupMemberText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorGroupMemberLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, target: "Position3", prefix: "public class ");

    private static GeneratorVerifier AssertVectorGroupMember() => AssertExactlyTypeNotPartialDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(VectorGroupMemberLocation, VectorGroupMemberText);
    private static Task AssertAndVerifyVectorGroupMember() => AssertVectorGroupMember().VerifyDiagnostics();
}
