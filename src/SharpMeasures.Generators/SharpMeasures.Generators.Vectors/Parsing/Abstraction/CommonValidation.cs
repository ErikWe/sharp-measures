namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;
using System.Linq;

internal static class CommonValidation
{
    public static IResultWithDiagnostics<IReadOnlyList<QuantityOperationDefinition>> ValidateOperations(DefinedType type, IReadOnlyList<int> dimensions, IReadOnlyList<QuantityOperationDefinition> operations, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        var validationContext = new QuantityOperationValidationContext(type, QuantityType.Vector, dimensions, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(new QuantityOperationValidator(diagnosticsStrategy.QuantityOperationDiagnostics)).Filter(validationContext, operations);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorOperationDefinition>> ValidateVectorOperations(DefinedType type, IReadOnlyList<int> dimensions, IReadOnlyList<VectorOperationDefinition> vectorOperations, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        var validationContext = new VectorOperationValidationContext(type, dimensions, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(new VectorOperationValidator(diagnosticsStrategy.VectorOperationDiagnostics)).Filter(validationContext, vectorOperations);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ValidateConstants(DefinedType type, int dimension, IUnitType unit, IEnumerable<IUnitInstance> includedUnits, IReadOnlyList<VectorConstantDefinition> constants, IEnumerable<IVectorConstant> inheritedConstants, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select(static (unit) => unit.PluralForm));

        var validationContext = new VectorConstantValidationContext(type, dimension, unit, inheritedConstantNames, inheritedConstantMultiples, incluedUnitPlurals);

        return ProcessingFilter.Create(new VectorConstantValidator(diagnosticsStrategy.VectorConstantDiagnostics)).Filter(validationContext, constants);
    }
    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(DefinedType type, IReadOnlyList<ConvertibleVectorDefinition> conversions, IVectorPopulation vectorPopulation, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(type, VectorType.Group, vectorPopulation);

        return ValidateConversions(conversions, filteringContext, diagnosticsStrategy);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(DefinedType type, int dimension, IReadOnlyList<ConvertibleVectorDefinition> conversions, IVectorPopulation vectorPopulation, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(type, dimension, VectorType.Vector, vectorPopulation);

        return ValidateConversions(conversions, filteringContext, diagnosticsStrategy);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(IReadOnlyList<ConvertibleVectorDefinition> conversions, ConvertibleVectorFilteringContext context, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        return ProcessingFilter.Create(new ConvertibleVectorFilterer(diagnosticsStrategy.ConvertibleVectorDiagnostics)).Filter(context, conversions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(DefinedType type, IUnitType unit, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, HashSet<string> inheritedUnits, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        var filteringContext = new IncludeUnitsFilteringContext(type, unit, inheritedUnits);

        return ProcessingFilter.Create(new IncludeUnitsFilterer(diagnosticsStrategy.IncludeUnitsDiagnostics)).Filter(filteringContext, unitInstanceInclusions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(DefinedType type, IUnitType unit, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions, HashSet<string> inheritedUnits, IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(type, unit, inheritedUnits);

        return ProcessingFilter.Create(new ExcludeUnitsFilterer(diagnosticsStrategy.ExcludeUnitsDiagnostics)).Filter(filteringContext, unitInstanceExclusions);
    }
}
