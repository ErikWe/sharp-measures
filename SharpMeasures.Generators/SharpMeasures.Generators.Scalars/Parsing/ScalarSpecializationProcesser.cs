namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

internal sealed class ScalarSpecializationProcesser : AScalarProcesser<RawScalarSpecializationType, RawSpecializedSharpMeasuresScalarDefinition, ScalarSpecializationType, SpecializedSharpMeasuresScalarDefinition>
{
    public ScalarSpecializationProcesser(IScalarProcessingDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override ScalarSpecializationType ProduceResult(DefinedType type, SpecializedSharpMeasuresScalarDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<ScalarConstantDefinition> constants,
        IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, processes, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetUnit(SpecializedSharpMeasuresScalarDefinition scalar) => null;
    protected override NamedType? GetOriginalQuantity(SpecializedSharpMeasuresScalarDefinition scalar) => scalar.OriginalQuantity;
    protected override bool ConversionFromOriginalQuantitySpecified(SpecializedSharpMeasuresScalarDefinition scalar) => scalar.Locations.ExplicitlySetForwardsCastOperatorBehaviour;
    protected override bool ConversionToOriginalQuantitySpecified(SpecializedSharpMeasuresScalarDefinition scalar) => scalar.Locations.ExplicitlySetBackwardsCastOperatorBehaviour;

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ProcessScalar(DefinedType type, RawSpecializedSharpMeasuresScalarDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresScalarProcesser).Filter(processingContext, rawDefinition);
    }

    private SpecializedSharpMeasuresScalarProcesser SpecializedSharpMeasuresScalarProcesser => new(DiagnosticsStrategy.SpecializedSharpMeasuresScalarDiagnostics);
}
