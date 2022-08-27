namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

internal record class BiasedUnitValidationContext : DependantUnitValidationContext, IBiasedUnitValidationContext
{
    public bool UnitIncludesBiasTerm { get; }

    public BiasedUnitValidationContext(DefinedType type, bool unitIncludesBiasTerm, IReadOnlyDictionary<string, IUnitInstance> unitsByName, HashSet<IDependantUnitInstance> cyclicDependantUnits) : base(type, unitsByName, cyclicDependantUnits)
    {
        UnitIncludesBiasTerm = unitIncludesBiasTerm;
    }
}
