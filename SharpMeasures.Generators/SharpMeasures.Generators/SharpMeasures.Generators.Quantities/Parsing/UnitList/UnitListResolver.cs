namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

public interface IUnitListResolutionDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IUnitListResolutionContext context, UnresolvedUnitListDefinition definition, int index);
}

public interface IUnitListResolutionContext : IProcessingContext
{
    public IRawUnitType Unit { get; }
}

public class UnitListResolver : IProcesser<IUnitListResolutionContext, UnresolvedUnitListDefinition, UnitListDefinition>
{
    private IUnitListResolutionDiagnostics Diagnostics { get; }

    public UnitListResolver(IUnitListResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<UnitListDefinition> Process(IUnitListResolutionContext context, UnresolvedUnitListDefinition definition)
    {
        List<Diagnostic> allDiagnostics = new();
        List<IRawUnitInstance> units = new();

        for (var i = 0; i < definition.Units.Count; i++)
        {
            if (context.Unit.UnitsByName.TryGetValue(definition.Units[i], out var unit) is false)
            {
                if (Diagnostics.UnrecognizedUnit(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            units.Add(unit);
        }

        UnitListDefinition product = new(units);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
