namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnmatchedProcessParameterDefinitions
{
    [Fact]
    public Task VerifyUnmatchedProcessParameterDefinitionsDiagnosticsMessage() => AssertScalar(MoreTypes).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(UnmatchedDefinitions))]
    public void Scalar(TextConfig definitions) => AssertScalar(definitions);

    [Theory]
    [MemberData(nameof(UnmatchedDefinitions))]
    public void SpecializedScalar(TextConfig definitions) => AssertSpecializedScalar(definitions);

    [Theory]
    [MemberData(nameof(UnmatchedDefinitions))]
    public void Vector(TextConfig definitions) => AssertVector(definitions);

    [Theory]
    [MemberData(nameof(UnmatchedDefinitions))]
    public void SpecializedVector(TextConfig definitions) => AssertSpecializedVector(definitions);

    [Theory]
    [MemberData(nameof(UnmatchedDefinitions))]
    public void VectorGroupMember(TextConfig definitions) => AssertVectorGroupMember(definitions);

    public static IEnumerable<object[]> UnmatchedDefinitions() => new object[][]
    {
        new object[] { MoreTypes },
        new object[] { MoreNames }
    };

    private static TextConfig MoreTypes => new("typeof(int), typeof(string)", "\"x\"");
    private static TextConfig MoreNames => new("typeof(int)", "\"x\", \"y\"");

    public readonly record struct TextConfig(string ParameterTypes, string ParameterNames);

    private static GeneratorVerifier AssertExactlyUnmatchedProcessParameterDefinitionsDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnmatchedProcessParameterDefinitionsDiagnostics);
    private static IReadOnlyCollection<string> UnmatchedProcessParameterDefinitionsDiagnostics { get; } = new string[] { DiagnosticIDs.UnmatchedProcessParameterDefinitions };

    private static string ScalarText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig definitions)
    {
        var source = ScalarText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityProcess");

        return AssertExactlyUnmatchedProcessParameterDefinitionsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig definitions)
    {
        var source = SpecializedScalarText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityProcess");

        return AssertExactlyUnmatchedProcessParameterDefinitionsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig definitions)
    {
        var source = VectorText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityProcess");

        return AssertExactlyUnmatchedProcessParameterDefinitionsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig definitions)
    {
        var source = SpecializedVectorText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityProcess");

        return AssertExactlyUnmatchedProcessParameterDefinitionsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig definitions)
    {
        var source = VectorGroupMemberText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityProcess");

        return AssertExactlyUnmatchedProcessParameterDefinitionsDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
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
