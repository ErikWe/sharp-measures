namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorConstantInvalidDimension
{
    [Fact]
    public Task Implicit_TooFew_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorConstantInvalidDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Implicit_TooMany_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorConstantInvalidDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Explicit_TooFew_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength), Dimension = 3)]
            public partial class LengthVector { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorConstantInvalidDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Explicit_TooMany_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength), Dimension = 3)]
            public partial class LengthVector { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorConstantInvalidDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyVectorConstantInvalidDimensionDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorConstantInvalidDimensionDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> VectorConstantInvalidDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.VectorConstantInvalidDimension };
}
