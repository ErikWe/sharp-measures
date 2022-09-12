namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyUnrecognizedPrefixDiagnosticsMessage() => AssertPrefixedUnitInstance(NegativeOne, "MetricPrefixName").VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void PrefixedUnitInstance_Binary(SourceSubtext prefix) => AssertPrefixedUnitInstance(prefix, "BinaryPrefixName");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void PrefixedUnitInstance_Metric(SourceSubtext prefix) => AssertPrefixedUnitInstance(prefix, "MetricPrefixName");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour(castOperationBehaviour);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void InclusionStackingMode_Unit(SourceSubtext inclusionStackingMode) => AssertInclusionStackingMode_Unit(inclusionStackingMode);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void InclusionStackingMode_Base(SourceSubtext inclusionStackingMode) => AssertInclusionStackingMode_Base(inclusionStackingMode);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void DerivedQuantity(SourceSubtext operatorImplementation) => AssertDerivedQuantity(operatorImplementation);

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

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", ({{type}}){{prefix}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixedUnitInstance(SourceSubtext prefix, string type)
    {
        var source = PrefixedUnitText(prefix, type);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, prefix.Context.With(outerPrefix: $"PrefixedUnitInstance(\"Kilometre\", \"Kilometres\", \"Metre\", ({type})"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(PrefixUnitIdentical);
    }

    private static string CastOperatorBehaviourText(SourceSubtext castOperatorBehaviour) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = (ConversionOperatorBehaviour){{castOperatorBehaviour}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertCastOperatorBehaviour(SourceSubtext castOperatorBehaviour)
    {
        var source = CastOperatorBehaviourText(castOperatorBehaviour);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: "CastOperatorBehaviour = (ConversionOperatorBehaviour)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(CastOperatorBehaviourIdentical);
    }

    private static string InclusionStackingModeText_Unit(SourceSubtext inclusionStackingMode) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnits("Metre", StackingMode = (InclusionStackingMode){{inclusionStackingMode}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertInclusionStackingMode_Unit(SourceSubtext inclusionStackingMode)
    {
        var source = InclusionStackingModeText_Unit(inclusionStackingMode);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, inclusionStackingMode.Context.With(outerPrefix: "StackingMode = (InclusionStackingMode)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(InclusionStackingModeIdentical_Unit);
    }

    private static string InclusionStackingModeText_Base(SourceSubtext inclusionStackingMode) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnitBases("Metre", StackingMode = (InclusionStackingMode){{inclusionStackingMode}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertInclusionStackingMode_Base(SourceSubtext inclusionStackingMode)
    {
        var source = InclusionStackingModeText_Base(inclusionStackingMode);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, inclusionStackingMode.Context.With(outerPrefix: "StackingMode = (InclusionStackingMode)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(InclusionStackingModeIdentical_Base);
    }

    private static string DerivedQuantityText(SourceSubtext operatorImplementation) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [DerivedQuantity("{0}", typeof(Length), OperatorImplementation = (DerivationOperatorImplementation){{operatorImplementation}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertDerivedQuantity(SourceSubtext inclusionStackingMode)
    {
        var source = DerivedQuantityText(inclusionStackingMode);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, inclusionStackingMode.Context.With(outerPrefix: "OperatorImplementation = (DerivationOperatorImplementation)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivedQuantityIdentical);
    }

    private static GeneratorVerifier PrefixUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(PrefixedUnitIdenticalText);
    private static GeneratorVerifier CastOperatorBehaviourIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(CastOperatorBehaviourIdenticalText);
    private static GeneratorVerifier InclusionStackingModeIdentical_Unit => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InclusionStackingModeIdenticalText_Unit);
    private static GeneratorVerifier InclusionStackingModeIdentical_Base => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InclusionStackingModeIdenticalText_Base);
    private static GeneratorVerifier DerivedQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivedQuantityIdenticalText);

    private static string PrefixedUnitIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string CastOperatorBehaviourIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string InclusionStackingModeIdenticalText_Unit => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string InclusionStackingModeIdenticalText_Base => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string DerivedQuantityIdenticalText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [DerivedQuantity("{0}", typeof(Length), OperatorImplementation = DerivationOperatorImplementation.None)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
