namespace SharpMeasures.Generators.Tests.Diagnostics.NoDiagnostics;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class IncludeUnitAndBase
{
    [Fact]
    public void Scalar() => AssertNoDiagnostics(ScalarText);

    [Fact]
    public void SpecializedScalar() => AssertNoDiagnostics(SpecializedScalarText);

    private static GeneratorVerifier AssertNoDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported();

    private static string ScalarText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnits("Metre")]
        [IncludeUnitBases("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnits("Metre")]
        [IncludeUnitBases("Metre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
