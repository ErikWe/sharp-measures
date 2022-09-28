namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NullProcessParameterType
{
    [Fact]
    public Task VerifyNullProcessParameterTypeDiagnosticsMessage() => AssertScalar(SingleParameter).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(NullParameterTypes))]
    public void Scalar(TextConfig definitions) => AssertScalar(definitions);

    [Theory]
    [MemberData(nameof(NullParameterTypes))]
    public void SpecializedScalar(TextConfig definitions) => AssertSpecializedScalar(definitions);

    [Theory]
    [MemberData(nameof(NullParameterTypes))]
    public void Vector(TextConfig definitions) => AssertVector(definitions);

    [Theory]
    [MemberData(nameof(NullParameterTypes))]
    public void SpecializedVector(TextConfig definitions) => AssertSpecializedVector(definitions);

    [Theory]
    [MemberData(nameof(NullParameterTypes))]
    public void VectorGroupMember(TextConfig definitions) => AssertVectorGroupMember(definitions);

    public static IEnumerable<object[]> NullParameterTypes() => new object[][]
    {
        new object[] { SingleParameter },
        new object[] { SecondParameter }
    };

    private static TextConfig SingleParameter => new("null", "\"x\"");
    private static TextConfig SecondParameter => new("typeof(int), null", "\"x\", \"y\"");

    public readonly record struct TextConfig(string ParameterTypes, string ParameterNames);

    private static GeneratorVerifier AssertExactlyNullProcessParameterTypeDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(NullProcessParameterTypeDiagnostics);
    private static IReadOnlyCollection<string> NullProcessParameterTypeDiagnostics { get; } = new string[] { DiagnosticIDs.NullProcessParameterType };

    private static string ScalarText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ProcessedQuantity("Name", "new(Magnitude)", new System.Type[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig definitions)
    {
        var source = ScalarText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "null");

        return AssertExactlyNullProcessParameterTypeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [ProcessedQuantity("Name", "new(Magnitude)", new System.Type[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig definitions)
    {
        var source = SpecializedScalarText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "null");

        return AssertExactlyNullProcessParameterTypeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ProcessedQuantity("Name", "new(Magnitude)", new System.Type[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig definitions)
    {
        var source = VectorText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "null");

        return AssertExactlyNullProcessParameterTypeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ProcessedQuantity("Name", "new(Magnitude)", new System.Type[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig definitions)
    {
        var source = SpecializedVectorText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "null");

        return AssertExactlyNullProcessParameterTypeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ProcessedQuantity("Name", "new(Magnitude)", new System.Type[] { {{definitions.ParameterTypes}} }, new[] { {{definitions.ParameterNames}} })]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig definitions)
    {
        var source = VectorGroupMemberText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "null");

        return AssertExactlyNullProcessParameterTypeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
