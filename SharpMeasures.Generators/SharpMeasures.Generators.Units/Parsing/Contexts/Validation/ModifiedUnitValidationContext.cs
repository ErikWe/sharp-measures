namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class ModifiedUnitValidationContext : SimpleProcessingContext, IModifiedUnitInstanceValidationContext
{
    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName { get; }

    public HashSet<IModifiedUnitInstance> CyclicallyModifiedUnits { get; }

    public ModifiedUnitValidationContext(DefinedType type, IReadOnlyDictionary<string, IUnitInstance> unitInstancesByName, HashSet<IModifiedUnitInstance> cyclicallyModifiedUnits) : base(type)
    {
        UnitInstancesByName = unitInstancesByName;

        CyclicallyModifiedUnits = cyclicallyModifiedUnits;
    }
}
