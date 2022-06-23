namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateConstantMultiplesName
{
    [Fact]
    public Task Scalar_Explicit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "Plancks")]
            [ScalarConstant("Planck2", "Metre", 1.616255E-35, Multiples = "Plancks")]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ImplicitFirst_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Planck", "Metre", 1.616255E-35)]
            [ScalarConstant("Planck2", "Metre", 1.616255E-35, Multiples = "MultiplesOfPlanck")]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ExplicitFirst_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Planck2", "Metre", 1.616255E-35, Multiples = "MultiplesOfPlanck")]
            [ScalarConstant("Planck", "Metre", 1.616255E-35)]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_Explicit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "InMetreOnes")]
            [VectorConstant("MetreOnes2", "Metre", 1, 1, 1, Multiples = "InMetreOnes")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ImplicitFirst_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
            [VectorConstant("MetreOnes2", "Metre", 1, 1, 1, Multiples = "MultiplesOfMetreOnes")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExplicitFirst_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes2", "Metre", 1, 1, 1, Multiples = "MultiplesOfMetreOnes")]
            [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDuplicateConstantMultiplesNameDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantMultiplesNameDiagnostics);

    private static IReadOnlyCollection<string> DuplicateConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantMultiplesName };
}
