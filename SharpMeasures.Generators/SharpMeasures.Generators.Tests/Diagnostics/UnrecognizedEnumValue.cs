﻿namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedEnumValue
{
    [Fact]
    public Task VerifyUnrecognizedPrefixDiagnosticsMessage() => AssertPrefixedUnit(NegativeOne, "MetricPrefixName").VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void PrefixedUnit_Binary(SourceSubtext prefix) => AssertPrefixedUnit(prefix, "BinaryPrefixName");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void PrefixedUnit_Metric(SourceSubtext prefix) => AssertPrefixedUnit(prefix, "MetricPrefixName");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour(castOperationBehaviour);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void InclusionStackingMode_Unit(SourceSubtext inclusionStackingMode) => AssertInclusionStackingMode_Unit(inclusionStackingMode);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void InclusionStackingMode_Base(SourceSubtext inclusionStackingMode) => AssertInclusionStackingMode_Base(inclusionStackingMode);

    public static IEnumerable<object[]> UnrecognizedEnumValues => new object[][]
    {
        new object[] { NegativeOne },
        new object[] { IntegerMaxValue }
    };

    private static SourceSubtext NegativeOne { get; } = SourceSubtext.Covered("-1", prefix: "(", postfix: ")");
    private static SourceSubtext IntegerMaxValue { get; } = SourceSubtext.Covered("int.MaxValue");

    private static GeneratorVerifier AssertExactlyUnrecognizedEnumValueDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedEnumValueDiagnostics);
    private static IReadOnlyCollection<string> UnrecognizedEnumValueDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedEnumValue };

    private static string PrefixedUnitText(SourceSubtext prefix, string type) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres", 1)]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", ({{type}}){{prefix}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixedUnit(SourceSubtext prefix, string type)
    {
        var source = PrefixedUnitText(prefix, type);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, prefix.Context.With(outerPrefix: $"PrefixedUnit(\"Kilometre\", \"Kilometres\", \"Metre\", ({type})"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string CastOperatorBehaviourText(SourceSubtext castOperatorBehaviour) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Utility;

        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = (ConversionOperatorBehaviour){{castOperatorBehaviour}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertCastOperatorBehaviour(SourceSubtext castOperatorBehaviour)
    {
        var source = CastOperatorBehaviourText(castOperatorBehaviour);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: "CastOperatorBehaviour = (ConversionOperatorBehaviour)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string InclusionStackingModeText_Unit(SourceSubtext inclusionStackingMode) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Utility;

        [IncludeUnits("Metre", StackingMode = (InclusionStackingMode){{inclusionStackingMode}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertInclusionStackingMode_Unit(SourceSubtext inclusionStackingMode)
    {
        var source = InclusionStackingModeText_Unit(inclusionStackingMode);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, inclusionStackingMode.Context.With(outerPrefix: "StackingMode = (InclusionStackingMode)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string InclusionStackingModeText_Base(SourceSubtext inclusionStackingMode) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Utility;

        [IncludeBases("Metre", StackingMode = (InclusionStackingMode){{inclusionStackingMode}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertInclusionStackingMode_Base(SourceSubtext inclusionStackingMode)
    {
        var source = InclusionStackingModeText_Base(inclusionStackingMode);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, inclusionStackingMode.Context.With(outerPrefix: "StackingMode = (InclusionStackingMode)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}