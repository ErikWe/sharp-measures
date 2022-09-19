namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IExcludeUnitsFilteringDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnitInstance(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index);
    public abstract Diagnostic? UnitInstanceAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index);
}

public interface IExcludeUnitsFilteringContext : IProcessingContext
{
    public IUnitType UnitType { get; }

    public HashSet<string> IncludedUnitInstances { get; }
}

public sealed class ExcludeUnitsFilterer : AProcesser<IExcludeUnitsFilteringContext, ExcludeUnitsDefinition, ExcludeUnitsDefinition>
{
    private IExcludeUnitsFilteringDiagnostics Diagnostics { get; }

    public ExcludeUnitsFilterer(IExcludeUnitsFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ExcludeUnitsDefinition> Process(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition)
    {
        List<string> validUnitInstances = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.UnitInstances.Count; i++)
        {
            var validity = ValidateUnitInstanceRecognized(context, definition, i)
                .Validate(() => ValidateUnitInstanceNotAlreadyExcluded(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validUnitInstances.Add(definition.UnitInstances[i]);
                locationMap.Add(i);
            }
        }

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnitInstances.Count is not 0, () => new ExcludeUnitsDefinition(validUnitInstances, definition.Locations, locationMap), allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateUnitInstanceRecognized(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        var recognizedUnitInstance = context.UnitType.UnitInstancesByName.ContainsKey(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnitInstance, () => Diagnostics.UnrecognizedUnitInstance(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNotAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index)
    {
        var unitInstanceNotAlreadyExcluded = context.IncludedUnitInstances.Contains(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(unitInstanceNotAlreadyExcluded, () => Diagnostics.UnitInstanceAlreadyExcluded(context, definition, index));
    }
}
