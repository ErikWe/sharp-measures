namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal sealed class GroupSpecializationValidator : AGroupValidator<GroupSpecializationType, SpecializedSharpMeasuresVectorGroupDefinition>
{
    public GroupSpecializationValidator(IVectorValidationDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override GroupSpecializationType ProduceResult(DefinedType type, SpecializedSharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversion, IReadOnlyList<IncludeUnitsDefinition> unitinstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, conversion, unitinstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> ValidateGroup(GroupSpecializationType groupType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresVectorGroupValidationContext(groupType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupValidator).Filter(validationContext, groupType.Definition);
    }

    private SpecializedSharpMeasuresVectorGroupValidator SpecializedSharpMeasuresVectorGroupValidator => new(DiagnosticsStrategy.SpecializedSharpMeasuresVectorGroupDiagnostics);
}
