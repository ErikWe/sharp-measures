namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidProcessName
{
    [Fact]
    public Task VerifyInvalidProcessNameDiagnosticsMessage_Null() => AssertScalar(NullName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidProcessNames))]
    public void Scalar(SourceSubtext processName) => AssertScalar(processName);

    [Theory]
    [MemberData(nameof(InvalidProcessNames))]
    public void SpecializedScalar(SourceSubtext processName) => AssertSpecializedScalar(processName);

    [Theory]
    [MemberData(nameof(InvalidProcessNames))]
    public void Vector(SourceSubtext processName) => AssertVector(processName);

    [Theory]
    [MemberData(nameof(InvalidProcessNames))]
    public void SpecializedVector(SourceSubtext processName) => AssertSpecializedVector(processName);

    [Theory]
    [MemberData(nameof(InvalidProcessNames))]
    public void VectorGroupMember(SourceSubtext processName) => AssertVectorGroupMember(processName);

    public static IEnumerable<object[]> InvalidProcessNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidProcessNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidProcessNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidProcessNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidProcessName };

    private static string ScalarText(SourceSubtext processName) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess({{processName}}, "new(Magnitude)")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext processName)
    {
        var source = ScalarText(processName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, processName.Context.With(outerPrefix: "QuantityProcess("));

        return AssertExactlyInvalidProcessNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext processName) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess({{processName}}, "new(Magnitude)")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext processName)
    {
        var source = SpecializedScalarText(processName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, processName.Context.With(outerPrefix: "QuantityProcess("));

        return AssertExactlyInvalidProcessNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(SourceSubtext processName) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess({{processName}}, "new(Components)")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext processName)
    {
        var source = VectorText(processName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, processName.Context.With(outerPrefix: "QuantityProcess("));

        return AssertExactlyInvalidProcessNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext processName) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess({{processName}}, "new(Components)")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext processName)
    {
        var source = SpecializedVectorText(processName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, processName.Context.With(outerPrefix: "QuantityProcess("));

        return AssertExactlyInvalidProcessNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(SourceSubtext processName) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess({{processName}}, "new(Components)")]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext processName)
    {
        var source = VectorGroupMemberText(processName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, processName.Context.With(outerPrefix: "QuantityProcess("));

        return AssertExactlyInvalidProcessNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

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

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;

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
