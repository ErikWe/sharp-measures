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
    public void CastOperatorBehaviour_SpecializedScalarForward(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour_SpecializedScalar(castOperationBehaviour, "ForwardsCastOperatorBehaviour");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour_SpecializedVectorBackward(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour_SpecializedScalar(castOperationBehaviour, "BackwardsCastOperatorBehaviour");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour_SpecializedVectorForward(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour_SpecializedVector(castOperationBehaviour, "ForwardsCastOperatorBehaviour");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour_SpecializedVectorGroupBackward(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour_SpecializedVector(castOperationBehaviour, "BackwardsCastOperatorBehaviour");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour_SpecializedVectorGroupForward(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour_SpecializedVectorGroup(castOperationBehaviour, "ForwardsCastOperatorBehaviour");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void CastOperatorBehaviour_SpecializedScalarBackward(SourceSubtext castOperationBehaviour) => AssertCastOperatorBehaviour_SpecializedVectorGroup(castOperationBehaviour, "BackwardsCastOperatorBehaviour");

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void InclusionStackingMode_Unit(SourceSubtext inclusionStackingMode) => AssertInclusionStackingMode_Unit(inclusionStackingMode);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void InclusionStackingMode_Base(SourceSubtext inclusionStackingMode) => AssertInclusionStackingMode_Base(inclusionStackingMode);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void QuantityOperation_Implementation(SourceSubtext implementation) => AssertQuantityOperation_Implementation(implementation);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void QuantityOperation_OperatorType(SourceSubtext operatorType) => AssertQuantityOperation_OperatorType(operatorType);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void QuantityOperation_Position(SourceSubtext position) => AssertQuantityOperation_Position(position);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void VectorOperation_OperatorType(SourceSubtext operatorType) => AssertVectorOperation_OperatorType(operatorType);

    [Theory]
    [MemberData(nameof(UnrecognizedEnumValues))]
    public void VectorOperation_Position(SourceSubtext position) => AssertVectorOperation_Position(position);

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
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", ({{type}}){{prefix}})]
        [Unit(typeof(Length))]
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

        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = (ConversionOperatorBehaviour){{castOperatorBehaviour}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertCastOperatorBehaviour(SourceSubtext castOperatorBehaviour)
    {
        var source = CastOperatorBehaviourText(castOperatorBehaviour);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: "CastOperatorBehaviour = (ConversionOperatorBehaviour)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(CastOperatorBehaviourIdentical);
    }

    private static string CastOperatorBehaviourText_SpecializedScalar(SourceSubtext castOperatorBehaviour, string property) => $$"""
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length), {{property}} = (ConversionOperatorBehaviour){{castOperatorBehaviour}})]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertCastOperatorBehaviour_SpecializedScalar(SourceSubtext castOperatorBehaviour, string property)
    {
        var source = CastOperatorBehaviourText_SpecializedScalar(castOperatorBehaviour, property);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: $"{property} = (ConversionOperatorBehaviour)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(CastOperatorBehaviourIdentical_SpecializedScalar);
    }

    private static string CastOperatorBehaviourText_SpecializedVector(SourceSubtext castOperatorBehaviour, string property) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), {{property}} = (ConversionOperatorBehaviour){{castOperatorBehaviour}})]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertCastOperatorBehaviour_SpecializedVector(SourceSubtext castOperatorBehaviour, string property)
    {
        var source = CastOperatorBehaviourText_SpecializedVector(castOperatorBehaviour, property);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: $"{property} = (ConversionOperatorBehaviour)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(CastOperatorBehaviourIdentical_SpecializedVector);
    }

    private static string CastOperatorBehaviourText_SpecializedVectorGroup(SourceSubtext castOperatorBehaviour, string property) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position), {{property}} = (ConversionOperatorBehaviour){{castOperatorBehaviour}})]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertCastOperatorBehaviour_SpecializedVectorGroup(SourceSubtext castOperatorBehaviour, string property)
    {
        var source = CastOperatorBehaviourText_SpecializedVectorGroup(castOperatorBehaviour, property);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: $"{property} = (ConversionOperatorBehaviour)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(CastOperatorBehaviourIdentical_SpecializedVectorGroup);
    }

    private static string InclusionStackingModeText_Unit(SourceSubtext inclusionStackingMode) => $$"""
        using SharpMeasures.Generators;

        [IncludeUnits("Metre", StackingMode = (InclusionStackingMode){{inclusionStackingMode}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
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

        [IncludeUnitBases("Metre", StackingMode = (InclusionStackingMode){{inclusionStackingMode}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertInclusionStackingMode_Base(SourceSubtext inclusionStackingMode)
    {
        var source = InclusionStackingModeText_Base(inclusionStackingMode);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, inclusionStackingMode.Context.With(outerPrefix: "StackingMode = (InclusionStackingMode)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(InclusionStackingModeIdentical_Base);
    }

    private static string QuantityOperationText_Implementation(SourceSubtext implementation) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Addition, Implementation = (QuantityOperationImplementation){{implementation}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_Implementation(SourceSubtext implementation)
    {
        var source = QuantityOperationText_Implementation(implementation);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, implementation.Context.With(outerPrefix: "Implementation = (QuantityOperationImplementation)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(QuantityOperationIdentical);
    }

    private static string QuantityOperationText_OperatorType(SourceSubtext operatorType) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), (OperatorType){{operatorType}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_OperatorType(SourceSubtext operatorType)
    {
        var source = QuantityOperationText_OperatorType(operatorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operatorType.Context.With(outerPrefix: "(OperatorType)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(QuantityOperationIdentical);
    }

    private static string QuantityOperationText_Position(SourceSubtext position) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Addition, (OperatorPosition){{position}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperation_Position(SourceSubtext position)
    {
        var source = QuantityOperationText_Position(position);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, position.Context.With(outerPrefix: "(OperatorPosition)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(QuantityOperationIdentical);
    }

    private static string VectorOperationText_OperatorType(SourceSubtext operatorType) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Position3), (VectorOperatorType){{operatorType}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_OperatorType(SourceSubtext operatorType)
    {
        var source = VectorOperationText_OperatorType(operatorType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, operatorType.Context.With(outerPrefix: "(VectorOperatorType)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorOperationIdentical);
    }

    private static string VectorOperationText_Position(SourceSubtext position) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Length), typeof(Position3), VectorOperatorType.Dot, (OperatorPosition){{position}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_Position(SourceSubtext position)
    {
        var source = VectorOperationText_Position(position);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, position.Context.With(outerPrefix: "(OperatorPosition)"));

        return AssertExactlyUnrecognizedEnumValueDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorOperationIdentical);
    }

    private static GeneratorVerifier PrefixUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(PrefixedUnitIdenticalText);
    private static GeneratorVerifier CastOperatorBehaviourIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(CastOperatorBehaviourIdenticalText);
    private static GeneratorVerifier CastOperatorBehaviourIdentical_SpecializedScalar => GeneratorVerifier.Construct<SharpMeasuresGenerator>(CastOperatorBehaviourIdenticalText_SpecializedScalar);
    private static GeneratorVerifier CastOperatorBehaviourIdentical_SpecializedVector => GeneratorVerifier.Construct<SharpMeasuresGenerator>(CastOperatorBehaviourIdenticalText_SpecializedVector);
    private static GeneratorVerifier CastOperatorBehaviourIdentical_SpecializedVectorGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(CastOperatorBehaviourIdenticalText_SpecializedVectorGroup);
    private static GeneratorVerifier InclusionStackingModeIdentical_Unit => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InclusionStackingModeIdenticalText_Unit);
    private static GeneratorVerifier InclusionStackingModeIdentical_Base => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InclusionStackingModeIdenticalText_Base);
    private static GeneratorVerifier QuantityOperationIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(QuantityOperationIdenticalText);
    private static GeneratorVerifier VectorOperationIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorOperationIdenticalText);

    private static string PrefixedUnitIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string CastOperatorBehaviourIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string CastOperatorBehaviourIdenticalText_SpecializedScalar => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string CastOperatorBehaviourIdenticalText_SpecializedVector => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string CastOperatorBehaviourIdenticalText_SpecializedVectorGroup => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string InclusionStackingModeIdenticalText_Unit => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string InclusionStackingModeIdenticalText_Base => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string QuantityOperationIdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorOperationIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
