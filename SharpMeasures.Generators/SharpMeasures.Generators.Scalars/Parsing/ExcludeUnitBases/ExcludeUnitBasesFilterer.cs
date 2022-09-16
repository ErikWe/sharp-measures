namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal interface IExcludeUnitBasesFilteringDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnitInstance(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index);
    public abstract Diagnostic? UnitInstanceAlreadyExcluded(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index);
}

internal interface IExcludeUnitBasesFilteringContext : IProcessingContext
{
    public IUnitType UnitType { get; }

    public HashSet<string> IncludedUnitInstances { get; }
}

internal class ExcludeUnitBasesFilterer : AProcesser<IExcludeUnitBasesFilteringContext, ExcludeUnitBasesDefinition, ExcludeUnitBasesDefinition>
{
    private IExcludeUnitBasesFilteringDiagnostics Diagnostics { get; }

    public ExcludeUnitBasesFilterer(IExcludeUnitBasesFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ExcludeUnitBasesDefinition> Process(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition)
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

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnitInstances.Count is not 0, () => new ExcludeUnitBasesDefinition(validUnitInstances, definition.Locations, locationMap), allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateUnitInstanceRecognized(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index)
    {
        var recognizedUnitInstances = context.UnitType.UnitInstancesByName.ContainsKey(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnitInstances, () => Diagnostics.UnrecognizedUnitInstance(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNotAlreadyExcluded(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index)
    {
        var unitInstanceNotAlreadyExcluded = context.IncludedUnitInstances.Contains(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(unitInstanceNotAlreadyExcluded, () => Diagnostics.UnitInstanceAlreadyExcluded(context, definition, index));
    }
}
