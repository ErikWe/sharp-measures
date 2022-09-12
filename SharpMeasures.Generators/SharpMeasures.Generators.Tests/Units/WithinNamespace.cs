namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithinNamespace
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).VerifyMatchingSourceNames("Test.Testing.Tests.UnitTests.UnitOfLength.Common.g.cs");

    private static string Text => """
        namespace Test.Testing.Tests.UnitTests;

        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
