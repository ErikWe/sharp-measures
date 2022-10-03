namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System.Collections.Generic;

internal sealed class ScalarBaseProcesser : AScalarProcesser<RawScalarBaseType, RawSharpMeasuresScalarDefinition, ScalarBaseType, SharpMeasuresScalarDefinition>
{
    public ScalarBaseProcesser(IScalarProcessingDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override ScalarBaseType ProduceResult(DefinedType type, SharpMeasuresScalarDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<ScalarConstantDefinition> constants,
        IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, processes, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetUnit(SharpMeasuresScalarDefinition scalar) => scalar.Unit;
    protected override NamedType? GetOriginalQuantity(SharpMeasuresScalarDefinition scalar) => null;
    protected override bool ConversionFromOriginalQuantitySpecified(SharpMeasuresScalarDefinition scalar) => false;
    protected override bool ConversionToOriginalQuantitySpecified(SharpMeasuresScalarDefinition scalar) => false;

    protected override IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> ProcessScalar(DefinedType type, RawSharpMeasuresScalarDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresScalarProcesser).Filter(processingContext, rawDefinition);
    }

    private SharpMeasuresScalarProcesser SharpMeasuresScalarProcesser => new(DiagnosticsStrategy.SharpMeasuresScalarDiagnostics);
}
