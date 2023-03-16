namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AttributeAlias
{
    [Fact]
    public void Assert() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().AssertAllListedSourceNamesGenerated($"UnitOfLength.Instances.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        using FixedUnit = SharpMeasures.Generators.FixedUnitInstanceAttribute;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
