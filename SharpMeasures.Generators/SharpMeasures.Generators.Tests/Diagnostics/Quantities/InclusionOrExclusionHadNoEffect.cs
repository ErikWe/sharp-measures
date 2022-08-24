namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InclusionOrExclusionHadNoEffect
{
    [Fact]
    public Task VerifyInclusionOrExclusionHadNoEffectDiagnosticsMessage() => AssertSameAttribute(IncludeBasesAttribute).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(Attributes))]
    public void SameAttribute(string attribute) => AssertSameAttribute(attribute);

    [Theory]
    [MemberData(nameof(Attributes))]
    public void MultipleAttributes(string attribute) => AssertMultipleAttributes(attribute);

    public static IEnumerable<object[]> Attributes => new object[][]
    {
        new[] { IncludeUnitsAttribute },
        new[] { ExcludeUnitsAttribute },
        new[] { IncludeBasesAttribute },
        new[] { ExcludeBasesAttribute }
    };

    private static string IncludeUnitsAttribute { get; } = "IncludeUnits";
    private static string ExcludeUnitsAttribute { get; } = "ExcludeUnits";
    private static string IncludeBasesAttribute { get; } = "IncludeBases";
    private static string ExcludeBasesAttribute { get; } = "ExcludeBases";

    private static GeneratorVerifier AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InclusionOrExclusionHadNoEffectDiagnostics);
    private static IReadOnlyCollection<string> InclusionOrExclusionHadNoEffectDiagnostics { get; } = new string[] { DiagnosticIDs.InclusionOrExclusionHadNoEffect };

    private static string SameAttributeText(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre", "Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSameAttribute(string attribute)
    {
        var source = SameAttributeText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string MultipleAttributesText(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertMultipleAttributes(string attribute)
    {
        var source = MultipleAttributesText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
