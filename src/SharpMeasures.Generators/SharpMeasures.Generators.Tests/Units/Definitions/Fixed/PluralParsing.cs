﻿namespace SharpMeasures.Generators.Tests.Units.Definitions.Fixed;

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
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "[*]s")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string RegexText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", ".$", PluralFormRegexSubstitution = "es")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
