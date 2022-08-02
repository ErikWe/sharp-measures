namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotQuantity
{
    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Null()
    {
        var source = DerivedScalarText("(System.Type)null");

        return AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Int()
    {
        var source = DerivedScalarText("typeof(int)");

        return AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void DerivedScalar_ExactList(string value)
    {
        if (value is "null")
        {
            value = "(System.Type)null";
        }

        string source = DerivedScalarText(value);

        AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> NonQuantityTypes() => new object[][]
    {
        new[] { "null" },
        new[] { "typeof(int)" },
        new[] { "typeof(UnitOfLength)" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotQuantityDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> TypeNotQuantityDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotQuantity };

    private static string DerivedScalarText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{0}", {{value}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
