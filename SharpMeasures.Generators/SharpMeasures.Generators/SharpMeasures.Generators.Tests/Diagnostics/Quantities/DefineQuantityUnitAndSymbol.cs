namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DefineQuantityUnitAndSymbol
{
    [Fact]
    public Task VerifyDefineQuantityUnitAndSymbolDiagnosticsMessage_OnlyUnit()
    {
        var source = ScalarText("DefaultUnitName = \"Metre\"");

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyDefineQuantityUnitAndSymbolDiagnosticsMessage_OnlySymbol()
    {
        var source = ScalarText("DefaultUnitSymbol = \"m\"");

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(Arguments))]
    public void Scalar_ExactList(string argument)
    {
        var source = ScalarText(argument);

        AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(Arguments))]
    public void SpecializedScalar_ExactList(string argument)
    {
        var source = SpecializedScalarText(argument);

        AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(Arguments))]
    public void Vector_ExactList(string argument)
    {
        var source = VectorText(argument);

        AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(Arguments))]
    public void SpecializedVector_ExactList(string argument)
    {
        var source = SpecializedVectorText(argument);

        AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(Arguments))]
    public void VectorGroup_ExactList(string argument)
    {
        var source = VectorGroupText(argument);

        AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(Arguments))]
    public void SpecializedVectorGroup_ExactList(string argument)
    {
        var source = SpecializedVectorGroupText(argument);

        AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> Arguments() => new object[][]
    {
        new[] { "DefaultUnitName = \"Metre\"" },
        new[] { "DefaultUnitSymbol = \"m\"" }
    };

    private static GeneratorVerifier AssertExactlyDefineQuantityUnitAndSymbolDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DefineQuantityUnitAndSymbolDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DefineQuantityUnitAndSymbolDiagnostics { get; } = new string[] { DiagnosticIDs.DefineQuantityUnitAndSymbol };

    private static string ScalarText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), {{argument}})]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length), {{argument}})]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), {{argument}})]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), {{argument}})]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), {{argument}})]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position), {{argument}})]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
