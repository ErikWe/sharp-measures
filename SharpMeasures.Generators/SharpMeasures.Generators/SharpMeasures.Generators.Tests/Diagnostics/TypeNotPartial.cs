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
    public Task Unit()
    {
        return AssertExactlyTypeNotPartialDiagnostics(UnitText).AssertDiagnosticsLocation(UnitLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar()
    {
        return AssertExactlyTypeNotPartialDiagnostics(ScalarText).AssertDiagnosticsLocation(ScalarLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedScalar()
    {
        return AssertExactlyTypeNotPartialDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(SpecializedScalarLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector()
    {
        return AssertExactlyTypeNotPartialDiagnostics(VectorText).AssertDiagnosticsLocation(VectorLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector()
    {
        return AssertExactlyTypeNotPartialDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(SpecializedVectorLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroup()
    {
        return AssertExactlyTypeNotPartialDiagnostics(VectorGroupText).AssertDiagnosticsLocation(VectorGroupLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVectorGroupy()
    {
        return AssertExactlyTypeNotPartialDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(SpecializedVectorGroupLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember()
    {
        return AssertExactlyTypeNotPartialDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(VectorGroupMemberLocation).VerifyDiagnostics();
    }

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

    private static IEnumerable<TextSpan> UnitLocation => ExpectedDiagnosticsLocation.TextSpan(UnitText, "UnitOfLength2", prefix: "public class ");

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

    private static IEnumerable<TextSpan> ScalarLocation => ExpectedDiagnosticsLocation.TextSpan(ScalarText, "Length2", prefix: "public class ");

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

    private static IEnumerable<TextSpan> SpecializedScalarLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, "Length2", prefix: "public class ");

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

    private static IEnumerable<TextSpan> VectorLocation => ExpectedDiagnosticsLocation.TextSpan(VectorText, "Position3", prefix: "public class ");

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

    private static IEnumerable<TextSpan> SpecializedVectorLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, "Displacement3", prefix: "public class ");

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

    private static IEnumerable<TextSpan> VectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, "Position", "public static class ");

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

    private static IEnumerable<TextSpan> SpecializedVectorGroupLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, "Displacement", "public static class ");

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

    private static IEnumerable<TextSpan> VectorGroupMemberLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, "Position3", "public class ");
}
