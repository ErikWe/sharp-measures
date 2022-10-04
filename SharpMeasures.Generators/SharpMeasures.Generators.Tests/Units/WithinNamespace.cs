namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithinNamespace
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Test.Testing.Tests.UnitTests.UnitOfLength.Common.g.cs");

    private static string Text => """
        namespace Test.Testing.Tests.UnitTests;

        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
