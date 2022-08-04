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
public class TypeStatic
{
    [Fact]
    public Task Unit()
    {
        return AssertExactlyTypeStaticDiagnostics(UnitText).AssertDiagnosticsLocation(UnitLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar()
    {
        return AssertExactlyTypeStaticDiagnostics(ScalarText).AssertDiagnosticsLocation(ScalarLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedScalar()
    {
        return AssertExactlyTypeStaticDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(SpecializedScalarLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector()
    {
        return AssertExactlyTypeStaticDiagnostics(VectorText).AssertDiagnosticsLocation(VectorLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector()
    {
        return AssertExactlyTypeStaticDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(SpecializedVectorLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember()
    {
        return AssertExactlyTypeStaticDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(VectorGroupMemberLocation).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeStaticDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeStaticDiagnostics);
    private static IReadOnlyCollection<string> TypeStaticDiagnostics { get; } = new string[] { DiagnosticIDs.TypeStatic };

    private static string UnitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresUnit(typeof(Length))]
        public static partial class UnitOfLength2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> UnitLocation => ExpectedDiagnosticsLocation.TextSpan(UnitText, "UnitOfLength2", "public static partial class ");

    private static string ScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public static partial class Length2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> ScalarLocation => ExpectedDiagnosticsLocation.TextSpan(ScalarText, "Length2", "public static partial class ");

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public static partial class Length2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedScalarLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, "Length2", "public static partial class ");

    private static string VectorText => """
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

    private static IEnumerable<TextSpan> VectorLocation => ExpectedDiagnosticsLocation.TextSpan(VectorText, "Position3", "public static partial class ");

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public static partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedVectorLocation => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, "Displacement3", "public static partial class ");

    private static string VectorGroupMemberText => """
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

    private static IEnumerable<TextSpan> VectorGroupMemberLocation => ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, "Position3", "public static partial class ");
}
