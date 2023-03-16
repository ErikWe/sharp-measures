namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public sealed record class QuantityConstantValidationContext : IQuantityConstantValidationContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> InheritedConstantNames { get; }
    public HashSet<string> InheritedConstantMultiples { get; }

    public HashSet<string> IncludedUnitInstancePluralForms { get; }

    public QuantityConstantValidationContext(DefinedType type, IUnitType unitType, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMultiples, HashSet<string> includedUnitInstancePluralForms)
    {
        Type = type;

        UnitType = unitType;

        InheritedConstantNames = inheritedConstantNames;
        InheritedConstantMultiples = inheritedConstantMultiples;

        IncludedUnitInstancePluralForms = includedUnitInstancePluralForms;
    }
}
