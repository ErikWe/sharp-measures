namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal sealed class ScalarSpecializationValidator : AScalarValidator<ScalarSpecializationType, SpecializedSharpMeasuresScalarDefinition>
{
    public ScalarSpecializationValidator(IScalarValidationDiagnosticsStrategy validationStrategy) : base(validationStrategy) { }

    protected override ScalarSpecializationType ProduceResult(DefinedType type, SpecializedSharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ProcessedQuantityDefinition> processes, IReadOnlyList<ScalarConstantDefinition> constants,
        IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> unitBaseInstanceInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> unitBaseInstanceExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, processes, constants, conversions, unitBaseInstanceInclusions, unitBaseInstanceExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ValidateScalar(ScalarSpecializationType scalarType, ScalarProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresScalarValidationContext(scalarType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SpecializedSharpMeasuresScalarValidator).Filter(validationContext, scalarType.Definition);
    }

    private SpecializedSharpMeasuresScalarValidator SpecializedSharpMeasuresScalarValidator => new(DiagnosticsStrategy.SpecializedSharpMeasuresScalarDiagnostics);
}
