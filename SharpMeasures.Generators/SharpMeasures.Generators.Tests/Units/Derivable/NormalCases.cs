namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task SimpleDerivation() => Verify(SimpleDerivationText);

    [Fact]
    public Task PermutedDerivation() => Verify(PermutedDerivationText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames("UnitOfSpeed.Derivable.g.cs");

    private static string SimpleDerivationText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
            
        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
            
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string PermutedDerivationText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
            
        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
            
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime), Permutations = true)]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
