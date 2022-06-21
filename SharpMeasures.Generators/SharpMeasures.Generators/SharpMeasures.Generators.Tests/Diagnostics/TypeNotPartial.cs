namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotPartial
{
    [Fact]
    public Task Unit_Class_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(typeof(int))]
            public class UnitOfLength { }
            """;

        return AssertExactlyTypeNotPartialDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Unit_PrivateClass_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Units;

            class Foo
            {
                [SharpMeasuresUnit(typeof(int))]
                private class UnitOfLength { }
            }
            """;

        return AssertExactlyTypeNotPartialDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotPartialDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotPartialDiagnostics);

    private static IReadOnlyCollection<string> TypeNotPartialDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotPartial };
}
