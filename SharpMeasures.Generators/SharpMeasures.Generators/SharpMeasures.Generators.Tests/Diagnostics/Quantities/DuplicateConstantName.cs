namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateConstantName
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
    public void VectorGroupMember() => AssertVectorGroupMember();

    private static GeneratorVerifier AssertExactlyDuplicateConstantNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantNameDiagnostics);
    private static IReadOnlyCollection<string> DuplicateConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantName };

    private static string ScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [ScalarConstant("Planck", "Metre", 1.616255E-35)] // <-
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "\"Planck\"", postfix: ", \"Metre\", 1.616255E-35)] // <-");

        return AssertExactlyDuplicateConstantNameDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation, ScalarText);
    }

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [ScalarConstant("Planck", "Metre", 1.616255E-35)] // <-
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "\"Planck\"", postfix: ", \"Metre\", 1.616255E-35)] // <-");

        return AssertExactlyDuplicateConstantNameDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation, SpecializedScalarText);
    }

    private static string VectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)] // <-
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "\"MetreOnes\"", postfix: ", \"Metre\", 1, 1, 1)] // <-");

        return AssertExactlyDuplicateConstantNameDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation, VectorText);
    }

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)] // <-
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "\"MetreOnes\"", postfix: ", \"Metre\", 1, 1, 1)] // <-");

        return AssertExactlyDuplicateConstantNameDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorText);
    }

    private static string VectorGroupMemberText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)] // <-
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, target: "\"MetreOnes\"", postfix: ", \"Metre\", 1, 1, 1)] // <-");

        return AssertExactlyDuplicateConstantNameDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation, VectorGroupMemberText);
    }
}
