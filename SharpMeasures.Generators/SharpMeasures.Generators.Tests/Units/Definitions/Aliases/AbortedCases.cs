namespace SharpMeasures.Generators.Tests.Units.Definitions.Aliases;

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

    [Fact]
    public void EmptyAliasOf() => Assert(aliasOf: "\"\"");

    [Fact]
    public void NullAliasOf() => Assert(aliasOf: "null");

    [Fact]
    public void DuplicateName() => Assert(name: "\"Metre\"");

    [Fact]
    public void DuplicatePlural() => Assert(plural: "\"Metres\"");

    [Fact]
    public void UnrecognizedAliasOf() => Assert(aliasOf: "\"Kilometre\"");

    private static string Text(string name, string plural, string aliasOf) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnit("Metre", "Metres", 1)]
        [UnitAlias({{name}}, {{plural}}, {{aliasOf}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Assert(string name = "\"Meter\"", string plural = "\"Meters\"", string aliasOf = "\"Metre\"")
    {
        string source = Text(name, plural, aliasOf);

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }
}
