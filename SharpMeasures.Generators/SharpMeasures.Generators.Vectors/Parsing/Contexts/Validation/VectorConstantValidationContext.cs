namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal sealed record class VectorConstantValidationContext : IVectorConstantValidationContext
{
    public DefinedType Type { get; }

    public int Dimension { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> InheritedConstantNames { get; }
    public HashSet<string> InheritedConstantMultiples { get; }

    public HashSet<string> IncludedUnitInstancePluralForms { get; }

    public VectorConstantValidationContext(DefinedType type, int dimension, IUnitType unitType, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMuliples, HashSet<string> includedUnitInstancePluralForms)
    {
        Type = type;

        Dimension = dimension;

        UnitType = unitType;

        InheritedConstantNames = inheritedConstantNames;
        InheritedConstantMultiples = inheritedConstantMuliples;

        IncludedUnitInstancePluralForms = includedUnitInstancePluralForms;
    }
}
