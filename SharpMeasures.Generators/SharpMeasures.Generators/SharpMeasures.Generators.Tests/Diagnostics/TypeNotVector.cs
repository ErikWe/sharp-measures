namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotVector
{
    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Null()
    {
        var source = ScalarVectorText("null");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Int()
    {
        var source = ScalarVectorText("typeof(int)");

        return AssertExactlyTypeNotVectorDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void ScalarVector_ExactList(string value)
    {
        string source = ScalarVectorText(value);

        AssertExactlyTypeNotVectorDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void SpecializedVectorOriginalVector_ExactList(string value)
    {
        string source = SpecializedVectorOriginalVectorText(value);

        AssertExactlyTypeNotVectorDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void VectorDifference_ExactList(string value)
    {
        string source = VectorDifferenceText(value);

        AssertExactlyTypeNotVectorDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void SpecializedVectorDifference_ExactList(string value)
    {
        string source = SpecializedVectorDifferenceText(value);

        AssertExactlyTypeNotVectorDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonVectorTypes))]
    public void ConvertibleQuantity_ExactList(string value)
    {
        if (value is "null")
        {
            value = "(System.Type)null";
        }

        var source = ConvertibleQuantityText(value);

        AssertExactlyTypeNotVectorDiagnostics(source);
    }

    private static IEnumerable<object[]> NonVectorTypes() => new object[][]
    {
        new[] { "null" },
        new[] { "typeof(int)" },
        new[] { "typeof(UnitOfLength)" },
        new[] { "typeof(Length)" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotVectorDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorDiagnostics);
    private static IReadOnlyCollection<string> TypeNotVectorDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVector };

    private static string ScalarVectorText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), Vector = {{value}})]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorOriginalVectorText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector({{value}})]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorDifferenceText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Difference = {{value}})]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorDifferenceText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), Difference = {{value}})]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleQuantityText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity({{value}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
