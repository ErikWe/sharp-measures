namespace SharpMeasures.Generators.Tests.Vectors;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateTypeDefinition
{
    [Fact]
    public void Vector() => Assert(VectorText);

    [Fact]
    public void SpecializedVector() => Assert(SpecializedVectorText);

    [Fact]
    public void VectorGroup() => Assert(VectorGroupText);

    [Fact]
    public void SpecializedVectorGroup() => Assert(SpecializedVectorGroupText);

    [Fact]
    public void VectorGroupMember() => Assert(VectorGroupMemberText);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.NoAssertions).AssertNoDiagnosticsReported();

    private static string VectorText => $$"""
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorText => $$"""
        using SharpMeasures.Generators;
        
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupText => $$"""
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupText => $$"""
        using SharpMeasures.Generators;
        
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberText => $$"""
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
