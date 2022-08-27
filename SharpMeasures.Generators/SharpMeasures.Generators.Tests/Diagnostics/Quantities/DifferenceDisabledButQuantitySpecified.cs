namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DifferenceDisabledButQuantitySpecified
{
    [Fact]
    public Task Scalar() => AssertScalar().VerifyDiagnostics();

    [Fact]
    public void SpecializedScalar() => AssertSpecializedScalar();

    [Fact]
    public void Vector() => AssertVector();

    [Fact]
    public void SpecializedVector() => AssertSpecializedVector();

    [Fact]
    public void VectorGroup() => AssertVectorGroup();

    [Fact]
    public void SpecializedVectorGroup() => AssertSpecializedVectorGroup();

    private static GeneratorVerifier AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DifferenceDisabledButQuantitySpecifiedDiagnostics);
    private static IReadOnlyCollection<string> DifferenceDisabledButQuantitySpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.DifferenceDisabledButQuantitySpecified };

    private static string ScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Length))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "Length", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation, ScalarText);
    }

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length), ImplementDifference = false, Difference = typeof(Distance))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "Distance", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation, SpecializedScalarText);
    }

    private static string VectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Position3))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "Position3", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation, VectorText);
    }

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), ImplementDifference = false, Difference = typeof(Displacement3))]
        public partial class Displacement3 { } 

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "Displacement3", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorText);
    }

    private static string VectorGroupText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Position))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "Position", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(VectorGroupText).AssertDiagnosticsLocation(expectedLocation, VectorGroupText);
    }

    private static string SpecializedVectorGroupText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position), ImplementDifference = false, Difference = typeof(Displacement))]
        public static partial class Displacement { } 

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "Displacement", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorGroupText);
    }
}
