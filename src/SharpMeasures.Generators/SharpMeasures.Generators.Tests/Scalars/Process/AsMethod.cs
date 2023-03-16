namespace SharpMeasures.Generators.Tests.Scalars.Process;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AsMethod
{
    [Fact]
    public Task WithoutParameters() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(WithoutParametersText).VerifyMatchingSourceNames("Length.Processes.g.cs");

    [Fact]
    public Task WithParameters() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(WithParametersText).VerifyMatchingSourceNames("Length.Processes.g.cs");

    private static string WithoutParametersText => """
        using SharpMeasures.Generators;

        [QuantityProcess("Double", "new(2 * Magnitude)")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string WithParametersText => """
        using SharpMeasures.Generators;

        [QuantityProcess("Scale", "new(Magnitude * x)", new[] { typeof(double) }, new[] { "x" })]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
