namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class EmptyList
{
    [Fact]
    public Task ConvertibleScalar_ExplicitlyEmpty_ExactListAndVerify()
    {
        string source = $$"""
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ConvertibleQuantity(new System.Type[] { })]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyEmptyListDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ConvertibleScalar_ImplicitlyEmpty_ExactListAndVerify()
    {
        string source = $$"""
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ConvertibleQuantity]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyEmptyListDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyEmptyListDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(EmptyListDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> EmptyListDiagnostics { get; } = new string[] { DiagnosticIDs.EmptyList };
}
