namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantName
{
    private static string ScalarText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        [ScalarConstant({{name}}, "Metre", 1.616255E-35)]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{name}}, "Metre", 1, 1, 1)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task Scalar_Null_ExactListAndVerify()
    {
        string source = ScalarText("null");

        return AssertExactlyInvalidConstantNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_Empty_ExactListAndVerify()
    {
        string source = ScalarText("\"\"");

        return AssertExactlyInvalidConstantNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_Null_ExactListAndVerify()
    {
        string source = VectorText("null");

        return AssertExactlyInvalidConstantNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_Empty_ExactListAndVerify()
    {
        string source = VectorText("\"\"");

        return AssertExactlyInvalidConstantNameDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidConstantNameDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantNameDiagnostics);

    private static IReadOnlyCollection<string> InvalidConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantName };
}
