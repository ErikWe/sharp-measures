namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotScalar
{
    [Fact]
    public Task UnitQuantity_Null_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(null)]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task UnitQuantity_Int_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(typeof(int))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    private static string ScalarArgumentText(string argument, string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), {{argument}} = {{value}})]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task ScalarReciprocal_Null_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Reciprocal", "null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarReciprocal_Int_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Reciprocal", "typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarSquare_Null_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Square", "null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarSquare_Int_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Square", "typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarCube_Null_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Cube", "null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarCube_Int_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Cube", "typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarSquareRoot_Null_ExactListAndVerify()
    {
        string source = ScalarArgumentText("SquareRoot", "null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarSquareRoot_Int_ExactListAndVerify()
    {
        string source = ScalarArgumentText("SquareRoot", "typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarCubeRoot_Null_ExactListAndVerify()
    {
        string source = ScalarArgumentText("CubeRoot", "null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarCubeRoot_Int_ExactListAndVerify()
    {
        string source = ScalarArgumentText("CubeRoot", "typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarDifference_Null_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Difference", "null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarDifference_Int_ExactListAndVerify()
    {
        string source = ScalarArgumentText("Difference", "typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    private static string DimensionalEquivalenceText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DimensionalEquivalence({{value}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task DimensionalEquivalence_Null_ExactListAndVerify()
    {
        string source = DimensionalEquivalenceText("typeof(Distance), null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task DimensionalEquivalence_Int_ExactListAndVerify()
    {
        string source = DimensionalEquivalenceText("typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    private static string VectorScalarText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Scalar = {{value}})]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task VectorScalar_Null_ExactListAndVerify()
    {
        string source = VectorScalarText("null");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorScalar_Int_ExactListAndVerify()
    {
        string source = VectorScalarText("typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotScalarDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotScalarDiagnostics);

    private static IReadOnlyCollection<string> TypeNotScalarDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotScalar };
}
