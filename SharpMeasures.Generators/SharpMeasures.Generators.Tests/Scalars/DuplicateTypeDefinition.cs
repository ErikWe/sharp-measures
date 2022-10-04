namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateTypeDefinition
{
    [Fact]
    public void Scalar() => Assert(ScalarText);

    [Fact]
    public void SpecializedScalar() => Assert(SpecializedScalarText);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.NoAssertions);

    private static string ScalarText => $$"""
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText => $$"""
        using SharpMeasures.Generators;
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
