﻿namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUseUnitBiasValue
{
    [Fact]
    public void Falsee() => AssertIdentical(FalseeText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, new GeneratorVerifierSettings(true, false)).AssertIdenticalSources(Identical);

    private static string FalseeText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength), UseUnitBias = falsee, DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Identical { get; } = GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}