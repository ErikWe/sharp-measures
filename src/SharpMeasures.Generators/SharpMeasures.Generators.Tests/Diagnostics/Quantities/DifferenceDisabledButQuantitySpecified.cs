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
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Length))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "Length", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length), ImplementDifference = false, Difference = typeof(Distance))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "Distance", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Position3))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "Position3", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), ImplementDifference = false, Difference = typeof(Displacement3))]
        public partial class Displacement3 { } 

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "Displacement3", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Position))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "Position", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(VectorGroupText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position), ImplementDifference = false, Difference = typeof(Displacement))]
        public static partial class Displacement { } 

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "Displacement", prefix: "Difference = typeof(");

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length), ImplementDifference = false)]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), ImplementDifference = false)]
        public partial class Displacement3 { } 

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength), ImplementDifference = false)]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position), ImplementDifference = false)]
        public static partial class Displacement { } 

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
