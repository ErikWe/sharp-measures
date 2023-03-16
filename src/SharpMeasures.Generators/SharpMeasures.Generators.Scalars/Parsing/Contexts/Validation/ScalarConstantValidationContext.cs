namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal sealed record class ScalarConstantValidationContext : IScalarConstantValidationContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> InheritedConstantNames { get; }
    public HashSet<string> InheritedConstantMultiples { get; }

    public HashSet<string> IncludedUnitInstanceNames { get; }
    public HashSet<string> IncludedUnitInstancePluralForms { get; }

    public ScalarConstantValidationContext(DefinedType type, IUnitType unitType, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMuliples, HashSet<string> includedUnitInstanceNames, HashSet<string> includedUnitInstancePluralForms)
    {
        Type = type;

        UnitType = unitType;

        InheritedConstantNames = inheritedConstantNames;
        InheritedConstantMultiples = inheritedConstantMuliples;

        IncludedUnitInstanceNames = includedUnitInstanceNames;
        IncludedUnitInstancePluralForms = includedUnitInstancePluralForms;
    }
}
