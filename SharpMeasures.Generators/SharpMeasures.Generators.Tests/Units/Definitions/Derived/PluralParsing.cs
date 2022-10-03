namespace SharpMeasures.Generators.Tests.Units.Definitions.Derived;

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

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [FixedUnitInstance("Second", "Seconds")]
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivedUnitInstance("Hertz", "[*]", new[] { "Second" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;

    private static string RegexText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [FixedUnitInstance("Second", "Seconds")]
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivedUnitInstance("Hertz", ".$", new[] { "Second" }, PluralFormRegexSubstitution = "z")]
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [FixedUnitInstance("Second", "Seconds")]
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivedUnitInstance("Hertz", "Hertz", new[] { "Second" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;
}
