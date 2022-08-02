namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ConstantSharesNameWithUnit
{
    [Fact]
    public Task Scalar_Singular_WithUnitSingular_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("OneKilometre", "Metre", 1000)]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_Singular_WithUnitPlural_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Kilometres", "Metre", 1000)]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ExplicitMultiples_WithUnitSingular_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Kilometre2", "Metre", 1000, Multiples = "OneKilometre")]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ExplicitMultiples_WithUnitPlural_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Kilometre2", "Metre", 1000, Multiples = "Kilometres")]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_ImplicitMultiples_WithUnitPlural_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Kilometre", "Metre", 1000)]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "MultiplesOfKilometre", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_Singular_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("Kilometres", "Metre", 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExplicitMultiples_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("Kilometre2", "Metre", 1, 1, 1, Multiples="Kilometres")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ImplicitMultiples_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("Kilometre", "Metre", 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "MultiplesOfKilometre", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyConstantSharesNameWithUnitDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ConstantSharesNameWithUnitDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> ConstantSharesNameWithUnitDiagnostics { get; } = new string[] { DiagnosticIDs.ConstantSharesNameWithUnit };
}
