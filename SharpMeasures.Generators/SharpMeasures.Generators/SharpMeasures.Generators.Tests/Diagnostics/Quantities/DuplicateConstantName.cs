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
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Planck", "Metre", 1.616255E-35)]
            [ScalarConstant("Planck", "Metre", 1.616255E-35)]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExactListAndVerify()
    {
        string source = """
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

        return AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDuplicateConstantNameDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantNameDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DuplicateConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantName };
}
