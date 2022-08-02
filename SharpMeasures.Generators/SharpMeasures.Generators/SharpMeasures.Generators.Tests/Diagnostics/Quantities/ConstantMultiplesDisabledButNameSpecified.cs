namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ConstantMultiplesBisabledButNameSpecified
{
    [Fact]
    public Task Scalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false, Multiples = "Plancks")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false, Multiples = "InMetreOnes")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ConstantMultiplesDisabledButNameSpecifiedDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> ConstantMultiplesDisabledButNameSpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.ConstantMultiplesDisabledButNameSpecified };
}
