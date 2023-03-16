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

    private static SourceSubtext OnlyName { get; } = SourceSubtext.Covered("\"Metre\"", prefix: "DefaultUnit = ");
    private static SourceSubtext OnlySymbol { get; } = SourceSubtext.Covered("\"m\"", prefix: "DefaultSymbol = ");

    private static GeneratorVerifier AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DefineQuantityUnitAndSymbolDiagnostics);
    private static IReadOnlyCollection<string> DefineQuantityUnitAndSymbolDiagnostics { get; } = new string[] { DiagnosticIDs.DefineQuantityUnitAndSymbol };

    private static string ScalarText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength), {{argument}})]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext argument)
    {
        var source = ScalarText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "ScalarQuantity(typeof(UnitOfLength), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length), {{argument}})]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext argument)
    {
        var source = SpecializedScalarText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SpecializedScalarQuantity(typeof(Length), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength), {{argument}})]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext argument)
    {
        var source = VectorText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "VectorQuantity(typeof(UnitOfLength), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), {{argument}})]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext argument)
    {
        var source = SpecializedVectorText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SpecializedVectorQuantity(typeof(Position3), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength), {{argument}})]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(SourceSubtext argument)
    {
        var source = VectorGroupText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "VectorGroup(typeof(UnitOfLength), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText(SourceSubtext argument) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position), {{argument}})]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(SourceSubtext argument)
    {
        var source = SpecializedVectorGroupText(argument);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argument.Context.With(outerPrefix: "SpecializedVectorGroup(typeof(Position), "));

        return AssertExactlyDefineQuantityUnitAndSymbolDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
