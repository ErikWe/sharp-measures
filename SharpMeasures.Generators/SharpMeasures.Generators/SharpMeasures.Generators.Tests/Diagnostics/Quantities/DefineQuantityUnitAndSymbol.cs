namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DefineQuantityUnitAndSymbol
{
    private static string ScalarText(string info) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), {{info}})]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string info) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresVector(typeof(UnitOfLength), {{info}})]
        public partial class Position3 { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task Scalar_OnlyUnit_ExactListAndVerify()
    {
        var source = ScalarText("DefaultUnitName = \"Metre\"");

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Scalar_OnlySymbol_ExactListAndVerify()
    {
        var source = ScalarText("DefaultUnitSymbol = \"m\"");

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_OnlyUnit_ExactListAndVerify()
    {
        var source = VectorText("DefaultUnitName = \"Metre\"");

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_OnlySymbol_ExactListAndVerify()
    {
        var source = VectorText("DefaultUnitSymbol = \"m\"");

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DefineQuantityUnitAndSymbolDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DefineQuantityUnitAndSymbolDiagnostics { get; } = new string[] { DiagnosticIDs.DefineQuantityUnitAndSymbol };
}
