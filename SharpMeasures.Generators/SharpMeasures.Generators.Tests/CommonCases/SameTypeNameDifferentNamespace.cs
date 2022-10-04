namespace SharpMeasures.Generators.Tests.CommonCases;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class SameTypeNameDifferentNamespace
{
    [Fact]
    public void AssertSourcesGenerated() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertAllListedSourceNamesGenerated("Length.Common.g.cs", "Test.Length.Common.g.cs");

    private static string Text => $$"""
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        namespace Test
        {
            [ScalarQuantity(typeof(UnitOfLength))]
            public partial class Length { }
        }
        """;
}
