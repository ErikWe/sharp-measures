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

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(Identical);

    private static string CustomShorthandText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivedUnitInstance("Hertz", "[*]", new[] { "Second" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;

    private static string RegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivedUnitInstance("Hertz", ".$", new[] { "Second" }, PluralFormRegexSubstitution = "z")]
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [FixedUnitInstance("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivedUnitInstance("Hertz", "Hertz", new[] { "Second" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;
}
