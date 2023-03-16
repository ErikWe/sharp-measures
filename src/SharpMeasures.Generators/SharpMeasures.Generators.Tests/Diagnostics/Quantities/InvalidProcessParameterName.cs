namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidProcessParameterName
{
    [Fact]
    public Task VerifyInvalidProcessParameterNameDiagnosticsMessage() => AssertScalar(NullSingleParameter).VerifyDiagnostics();

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
        new object[] { NullSingleParameter },
        new object[] { EmptySingleParameter },
        new object[] { NullSecondParameter },
        new object[] { EmptySecondParameter }
    };

    private static TextConfig NullSingleParameter => new("typeof(int)", "null", "null");
    private static TextConfig EmptySingleParameter => new("typeof(int)", "\"\"", "\"\"");
    private static TextConfig NullSecondParameter => new("typeof(int), typeof(string)", "\"x\", null", "null");
    private static TextConfig EmptySecondParameter => new("typeof(int), typeof(string)", "\"x\", \"\"", "\"\"");

    public readonly record struct TextConfig(string ParameterTypes, string ParameterNames, string TargetString);

    private static GeneratorVerifier AssertExactlyInvalidProcessParameterNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidProcessParameterNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidProcessParameterNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidProcessParameterName };

    private static string ScalarText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new string[] { {{definitions.ParameterNames}} })]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig definitions)
    {
        var source = ScalarText(definitions);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: definitions.TargetString);

        return AssertExactlyInvalidProcessParameterNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new string[] { {{definitions.ParameterNames}} })]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: definitions.TargetString);

        return AssertExactlyInvalidProcessParameterNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new string[] { {{definitions.ParameterNames}} })]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: definitions.TargetString);

        return AssertExactlyInvalidProcessParameterNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new string[] { {{definitions.ParameterNames}} })]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: definitions.TargetString);

        return AssertExactlyInvalidProcessParameterNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(TextConfig definitions) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)", new[] { {{definitions.ParameterTypes}} }, new string[] { {{definitions.ParameterNames}} })]
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
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: definitions.TargetString);

        return AssertExactlyInvalidProcessParameterNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
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
