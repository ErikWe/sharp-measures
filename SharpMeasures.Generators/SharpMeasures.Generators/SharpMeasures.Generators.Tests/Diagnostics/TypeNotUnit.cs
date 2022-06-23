namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotUnit
{
    private static string DerivableUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength2 { }

        [DerivableUnit("1", "{0}", {{value}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task DerivableUnit_Null_ExactListAndVerify()
    {
        string source = DerivableUnitText("null, typeof(UnitOfLength2)");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task DerivableUnit_Int_ExactListAndVerify()
    {
        string source = DerivableUnitText("typeof(int)");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    private static string ScalarUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar({{value}})]
        public partial class Length { }
        """;

    [Fact]
    public Task ScalarUnit_Null_ExactListAndVerify()
    {
        string source = ScalarUnitText("null");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarUnit_Int_ExactListAndVerify()
    {
        string source = ScalarUnitText("typeof(int)");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    private static string VectorUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector({{value}})]
        public partial class Position3 { }
        """;

    [Fact]
    public Task VectorUnit_Null_ExactListAndVerify()
    {
        string source = VectorUnitText("null");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorUnit_Int_ExactListAndVerify()
    {
        string source = VectorUnitText("typeof(int)");

        return AssertExactlyTypeNotUnitDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotUnitDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotUnitDiagnostics);

    private static IReadOnlyCollection<string> TypeNotUnitDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnit };
}
