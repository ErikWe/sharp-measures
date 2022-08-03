namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateConstantName
{
    [Fact]
    public Task Scalar_ExactListAndVerify()
    {
        return AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(ScalarText).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedScalar_ExactListAndVerify()
    {
        return AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(SpecializedScalarText).VerifyDiagnostics();
    }

    [Fact]
    public void Vector_ExactList()
    {
        AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(VectorText);
    }

    [Fact]
    public void SpecializedVector_ExactList()
    {
        AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(SpecializedVectorText);
    }

    [Fact]
    public void VectorGroupMember_ExactList()
    {
        AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(VectorGroupMemberText);
    }

    private static GeneratorVerifier AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantNameDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DuplicateConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantName };

    private static string ScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
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

    private static string VectorGroupMemberText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
