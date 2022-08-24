namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

internal record class DependantUnitValidationContext : SimpleProcessingContext, IDependantUnitValidationContext
{
    public IReadOnlyDictionary<string, IUnitInstance> UnitsByName { get; }

    public DependantUnitValidationContext(DefinedType type, IReadOnlyDictionary<string, IUnitInstance> unitsByName) : base(type)
    {
        UnitsByName = unitsByName;
    }
}
