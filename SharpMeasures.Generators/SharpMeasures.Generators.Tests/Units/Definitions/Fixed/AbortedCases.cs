namespace SharpMeasures.Generators.Tests.Units.Definitions.Fixed;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void EmptyName() => Assert(name: "\"\"");

    [Fact]
    public void NullName() => Assert(name: "null");

    [Fact]
    public void EmptyPlural() => Assert(plural: "\"\"");

    [Fact]
    public void NullPlural() => Assert(plural: "null");

    private static string Text(string name, string plural) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnit({{name}}, {{plural}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Assert(string name = "\"Metre\"", string plural = "\"Metres\"")
    {
        string source = Text(name, plural);

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertIdenticalSources(CommonResults.Length_NoDefinitions);
    }
}
