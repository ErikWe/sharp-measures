namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotVector
{
    private static string ScalarVectorText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), Vector = {{value}})]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task ScalarVector_Null_ExactListAndVerify()
    {
        string source = ScalarVectorText("null");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarVector_Int_ExactListAndVerify()
    {
        string source = ScalarVectorText("typeof(int)");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    private static string VectorDifferenceText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Difference = {{value}})]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task VectorDifference_Null_ExactListAndVerify()
    {
        string source = VectorDifferenceText("null");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorDifference_Int_ExactListAndVerify()
    {
        string source = VectorDifferenceText("typeof(int)");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    private static string DimensionalEquivalenceText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [DimensionalEquivalence({{value}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task DimensionalEquivalence_Null_ExactListAndVerify()
    {
        string source = DimensionalEquivalenceText("typeof(Displacement3), null");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task DimensionalEquivalence_Int_ExactListAndVerify()
    {
        string source = DimensionalEquivalenceText("typeof(int)");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotVectorDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorDiagnostics);

    private static IReadOnlyCollection<string> TypeNotVectorDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVector };
}
