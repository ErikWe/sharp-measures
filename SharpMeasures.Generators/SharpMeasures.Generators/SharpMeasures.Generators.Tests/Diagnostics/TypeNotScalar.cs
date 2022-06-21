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

    private static GeneratorVerifier AssertExactlyTypeNotScalarDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotScalarDiagnostics);

    private static IReadOnlyCollection<string> TypeNotScalarDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotScalar };
}
