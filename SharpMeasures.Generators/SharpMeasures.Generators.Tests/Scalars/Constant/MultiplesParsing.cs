namespace SharpMeasures.Generators.Tests.Scalars.Constants;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class MultiplesParsing
{
    [Fact]
    public void CustomShorthand() => AssertIdentical(CustomShorthandText);

    [Fact]
    public void Regex() => AssertIdentical(RegexText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().AssertIdenticalSources(Identical);

    private static string CustomShorthandText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "[*]Multiples")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string RegexText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = ".$", MultiplesRegexSubstitution = "kMultiples")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "PlanckMultiples")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
