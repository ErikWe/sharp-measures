namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotQuantity
{
    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Null() => AssertAndVerifyDerivedScalar(NullSubtext);

    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Int() => AssertAndVerifyDerivedScalar(IntSubtext);

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void DerivedScalar(SourceSubtext quantityType) => AssertDerivedScalar(quantityType);

    private static IEnumerable<object[]> NonQuantityTypes() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { IntSubtext },
        new object[] { UnitOfLengthSubtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null");
    private static SourceSubtext IntSubtext { get; } = SourceSubtext.Typeof("int");
    private static SourceSubtext UnitOfLengthSubtext { get; } = SourceSubtext.Typeof("UnitOfLength");

    private static GeneratorVerifier AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotQuantityDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> TypeNotQuantityDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotQuantity };

    private static string DerivedScalarText(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{0}", {{quantityType}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan DerivedScalarLocation(SourceSubtext quantityType) => ExpectedDiagnosticsLocation.TextSpan(DerivedScalarText(quantityType), quantityType, prefix: "DerivedQuantity(\"{0}\", ");

    private static GeneratorVerifier AssertDerivedScalar(SourceSubtext quantityType)
    {
        if (quantityType.Target is "null")
        {
            quantityType = quantityType with { Prefix = $"{quantityType.Prefix}(System.Type)" };
        }

        var source = DerivedScalarText(quantityType);
        var expectedLocation = DerivedScalarLocation(quantityType);

        return AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyDerivedScalar(SourceSubtext quantityType) => AssertDerivedScalar(quantityType).VerifyDiagnostics();
}
