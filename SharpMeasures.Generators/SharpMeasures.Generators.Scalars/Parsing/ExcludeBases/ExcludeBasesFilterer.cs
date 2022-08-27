namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal interface IExcludeBasesFilteringDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition, int index);
    public abstract Diagnostic? BaseAlreadyExcluded(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition, int index);
}

internal interface IExcludeBasesFilteringContext : IProcessingContext
{
    public IUnitType UnitType { get; }

    public HashSet<string> IncludedBases { get; }
}

internal class ExcludeBasesFilterer : AProcesser<IExcludeBasesFilteringContext, ExcludeBasesDefinition, ExcludeBasesDefinition>
{
    private IExcludeBasesFilteringDiagnostics Diagnostics { get; }

    public ExcludeBasesFilterer(IExcludeBasesFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ExcludeBasesDefinition> Process(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition)
    {
        List<string> validBases = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.ExcludedBases.Count; i++)
        {
            var validity = ValidateBaseRecognized(context, definition, i)
                .Validate(() => ValidateBaseNotAlreadyExcluded(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validBases.Add(definition.ExcludedBases[i]);
                locationMap.Add(i);
            }
        }

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validBases.Count is not 0, () => new ExcludeBasesDefinition(validBases, definition.Locations, locationMap), allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateBaseRecognized(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition, int index)
    {
        var recognizedBase = context.UnitType.UnitsByName.ContainsKey(definition.ExcludedBases[index]);

        return ValidityWithDiagnostics.Conditional(recognizedBase, () => Diagnostics.UnrecognizedUnit(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateBaseNotAlreadyExcluded(IExcludeBasesFilteringContext context, ExcludeBasesDefinition definition, int index)
    {
        var unitNotAlreadyExcluded = context.IncludedBases.Contains(definition.ExcludedBases[index]);

        return ValidityWithDiagnostics.Conditional(unitNotAlreadyExcluded, () => Diagnostics.BaseAlreadyExcluded(context, definition, index));
    }
}
