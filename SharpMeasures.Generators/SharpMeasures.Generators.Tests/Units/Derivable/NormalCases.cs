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

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfSpeed.Derivable.g.cs");

    private static string SimpleDerivationText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
            
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
            
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string PermutedDerivationText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
            
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
            
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime), Permutations = true)]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
