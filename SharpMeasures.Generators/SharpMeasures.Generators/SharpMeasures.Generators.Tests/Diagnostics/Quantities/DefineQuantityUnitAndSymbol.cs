namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DefineQuantityUnitAndSymbol
{
    [Fact]
    public Task VerifyDefineQuantityUnitAndSymbolDiagnosticsMessage_OnlyName() => AssertScalar(OnlyName).VerifyDiagnostics();

    [Fact]
    public Task VerifyDefineQuantityUnitAndSymbolDiagnosticsMessage_OnlySymbol() => AssertScalar(OnlySymbol).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(Arguments))]
    public void Scalar(SourceSubtext argument) => AssertScalar(argument);

    [Theory]
    [MemberData(nameof(Arguments))]
    public void SpecializedScalar(SourceSubtext argument) => AssertSpecializedScalar(argument);

    [Theory]
    [MemberData(nameof(Arguments))]
    public void Vector(SourceSubtext argument) => AssertVector(argument);

    [Theory]
    [MemberData(nameof(Arguments))]
    public void SpecializedVector(SourceSubtext argument) => AssertSpecializedVector(argument);

    [Theory]
    [MemberData(nameof(Arguments))]
    public void VectorGroup(SourceSubtext argument) => AssertVectorGroup(argument);

    [Theory]
    [MemberData(nameof(Arguments))]
    public void SpecializedVectorGroup(SourceSubtext argument) => AssertSpecializedVectorGroup(argument);

    public static IEnumerable<object[]> Arguments() => new object[][]
    {
        new object[] { OnlyName },
        new object[] { OnlySymbol }
    };

    private static SourceSubtext OnlyName { get; } = SourceSubtext.Covered("\"Metre\"", prefix: "DefaultUnitName = ");
    private static SourceSubtext OnlySymbol { get; } = SourceSubtext.Covered("\"m\"", prefix: "DefaultUnitSymbol = ");

    private static GeneratorVerifier AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DefineQuantityUnitAndSymbolDiagnostics);
    private static IReadOnlyCollection<string> DefineQuantityUnitAndSymbolDiagnostics { get; } = new string[] { DiagnosticIDs.DefineQuantityUnitAndSymbol };

    private static string ScalarText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), {{argument}})]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext argument)
    {
        var source = ScalarText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SharpMeasuresScalar(typeof(UnitOfLength), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarText(SourceSubtext argument) => $$"""
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

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext argument)
    {
        var source = SpecializedScalarText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SpecializedSharpMeasuresScalar(typeof(Length), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorText(SourceSubtext argument) => $$"""
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

    private static GeneratorVerifier AssertVector(SourceSubtext argument)
    {
        var source = VectorText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SharpMeasuresVector(typeof(UnitOfLength), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorText(SourceSubtext argument) => $$"""
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

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext argument)
    {
        var source = SpecializedVectorText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SpecializedSharpMeasuresVector(typeof(Position3), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupText(SourceSubtext argument) => $$"""
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

    private static GeneratorVerifier AssertVectorGroup(SourceSubtext argument)
    {
        var source = VectorGroupText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SharpMeasuresVectorGroup(typeof(UnitOfLength), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorGroupText(SourceSubtext argument) => $$"""
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

    private static GeneratorVerifier AssertSpecializedVectorGroup(SourceSubtext argument)
    {
        var source = SpecializedVectorGroupText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SpecializedSharpMeasuresVectorGroup(typeof(Position), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
