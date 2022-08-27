namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task Unit() => AssertUnit().VerifyDiagnostics();

    [Fact]
    public Task Scalar() => AssertScalar().VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar() => AssertSpecializedScalar().VerifyDiagnostics();

    [Fact]
    public Task Vector() => AssertVector().VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector() => AssertSpecializedVector().VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember() => AssertVectorGroupMember().VerifyDiagnostics();

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

    private static GeneratorVerifier AssertUnit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(UnitText, target: "UnitOfLength2", prefix: "public static partial class ");

        return AssertExactlyTypeStaticDiagnostics(UnitText).AssertDiagnosticsLocation(expectedLocation, UnitText);
    }

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

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "Length2", prefix: "public static partial class ");

        return AssertExactlyTypeStaticDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation, ScalarText);
    }

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

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "Length2", prefix: "public static partial class ");

        return AssertExactlyTypeStaticDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation, SpecializedScalarText);
    }

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

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "Position3", prefix: "public static partial class ");

        return AssertExactlyTypeStaticDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation, VectorText);
    }

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

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "Displacement3", prefix: "public static partial class ");

        return AssertExactlyTypeStaticDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorText);
    }

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

    private static GeneratorVerifier AssertVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, target: "Position3", prefix: "public static partial class ");

        return AssertExactlyTypeStaticDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation, VectorGroupMemberText);
    }
}
