namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantMultiplesName
{
    private static string ScalarText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{multiples}})]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{multiples}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task Scalar_Null_ExactListAndVerify()
    {
        string source = ScalarText("null");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_Empty_ExactListAndVerify()
    {
        string source = ScalarText("\"\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_Null_ExactListAndVerify()
    {
        string source = VectorText("null");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_Empty_ExactListAndVerify()
    {
        string source = VectorText("\"\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidConstantMultiplesNameDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantMultiplesNameDiagnostics);

    private static IReadOnlyCollection<string> InvalidConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantMultiplesName };
}
