namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VectorGroup() => AssertVectorGroup().VerifyDiagnostics();

    [Fact]
    public Task SpecializedVectorGroupy() => AssertSpecializedVectorGroup().VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember() => AssertVectorGroupMember().VerifyDiagnostics();

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

    private static GeneratorVerifier AssertUnit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(UnitText, target: "UnitOfLength2", prefix: "public class ");

        return AssertExactlyTypeNotPartialDiagnostics(UnitText).AssertDiagnosticsLocation(expectedLocation, UnitText);
    }

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

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "Length2", prefix: "public class ");

        return AssertExactlyTypeNotPartialDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation, ScalarText);
    }

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

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "Length2", prefix: "public class ");

        return AssertExactlyTypeNotPartialDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation, SpecializedScalarText);
    }

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

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "Position3", prefix: "public class ");

        return AssertExactlyTypeNotPartialDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation, VectorText);
    }

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

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "Displacement3", prefix: "public class ");

        return AssertExactlyTypeNotPartialDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorText);
    }

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

    private static GeneratorVerifier AssertVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "Position", prefix: "public static class ");

        return AssertExactlyTypeNotPartialDiagnostics(VectorGroupText).AssertDiagnosticsLocation(expectedLocation, VectorGroupText);
    }

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

    private static GeneratorVerifier AssertSpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "Displacement", prefix: "public static class ");

        return AssertExactlyTypeNotPartialDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorGroupText);
    }

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

    private static GeneratorVerifier AssertVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, target: "Position3", prefix: "public class ");

        return AssertExactlyTypeNotPartialDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation, VectorGroupMemberText);
    }
}
