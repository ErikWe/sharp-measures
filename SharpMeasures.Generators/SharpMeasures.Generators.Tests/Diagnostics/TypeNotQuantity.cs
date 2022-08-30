namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Null() => AssertDerivedScalar(NullType).VerifyDiagnostics();

    [Fact]
    public Task VerifyTypeNotQuantityDiagnosticsMessage_Int() => AssertDerivedScalar(IntType).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(NonQuantityTypes))]
    public void DerivedScalar(SourceSubtext quantityType) => AssertDerivedScalar(quantityType);

    public static IEnumerable<object[]> NonQuantityTypes() => new object[][]
    {
        new object[] { NullType },
        new object[] { IntType },
        new object[] { UnitOfLengthType }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", prefix: "(System.Type)");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int");
    private static SourceSubtext UnitOfLengthType { get; } = SourceSubtext.AsTypeof("UnitOfLength");

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

    private static GeneratorVerifier AssertDerivedScalar(SourceSubtext quantityType)
    {
        var source = DerivedScalarText(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "DerivedQuantity(\"{0}\", "));

        return AssertExactlyTypeNotQuantityDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivedScalarIdentical);
    }

    private static GeneratorVerifier DerivedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivedScalarIdenticalText);

    private static string DerivedScalarIdenticalText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
