namespace SharpMeasures.Generators.Tests.Diagnostics.Misc;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Debug
{
    [Fact]
    public void Test()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Displacement { }

            [SharpMeasuresVector(typeof(UnitOfLength), Difference = typeof(Displacement))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source);
    }
}
