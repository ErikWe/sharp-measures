namespace SharpMeasures.Generators.Tests.Units.Definitions.Scaled;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class PluralParsing
{
    [Fact]
    public void CustomShorthand() => AssertIdentical(CustomShorthandText);

    [Fact]
    public void Regex() => AssertIdentical(RegexText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().AssertIdenticalSources(Identical);

    private static string CustomShorthandText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [ScaledUnitInstance("Kilometre", "[*]s", "Metre", 1000)]
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string RegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [ScaledUnitInstance("Kilometre", ".$", "Metre", 1000, PluralFormRegexSubstitution = "es")]
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", 1000)]
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
