namespace SharpMeasures.Generators.Vectors.Parsing;

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
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal sealed class GroupBaseValidator : AGroupValidator<GroupBaseType, SharpMeasuresVectorGroupDefinition>
{
    public GroupBaseValidator(IVectorValidationDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override GroupBaseType ProduceResult(DefinedType type, SharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversion, IReadOnlyList<IncludeUnitsDefinition> unitinstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, conversion, unitinstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ValidateGroup(GroupBaseType groupType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorGroupValidationContext(groupType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupValidator).Filter(validationContext, groupType.Definition);
    }

    private SharpMeasuresVectorGroupValidator SharpMeasuresVectorGroupValidator => new(DiagnosticsStrategy.SharpMeasuresVectorGroupDiagnostics);
}
