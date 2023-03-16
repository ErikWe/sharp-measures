namespace SharpMeasures.Generators.Tests.Scalars.Process;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AsProperty
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).VerifyMatchingSourceNames("Length.Processes.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [QuantityProcess("Double", "new(2 * Magnitude)", ImplementAsProperty = true)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
