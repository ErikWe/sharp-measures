namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public record class QuantityConstantValidationContext : SimpleProcessingContext, IQuantityConstantValidationContext
{
    public IUnitType UnitType { get; }

    public HashSet<string> InheritedConstantNames { get; }
    public HashSet<string> InheritedConstantMultiples { get; }

    public HashSet<string> IncludedUnitInstancePluralForms { get; }

    public QuantityConstantValidationContext(DefinedType type, IUnitType unitType, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMultiples, HashSet<string> includedUnitInstancePluralForms) : base(type)
    {
        UnitType = unitType;

        InheritedConstantNames = inheritedConstantNames;
        InheritedConstantMultiples = inheritedConstantMultiples;

        IncludedUnitInstancePluralForms = includedUnitInstancePluralForms;
    }
}
