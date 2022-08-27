namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal record class ScalarConstantValidationContext : QuantityConstantValidationContext, IScalarConstantValidationContext
{
    public HashSet<string> IncludedUnitNames { get; }

    public ScalarConstantValidationContext(DefinedType type, IUnitType unitType, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMuliplesNames, HashSet<string> includedUnitNames, HashSet<string> includedUnitPlurals)
        : base(type, unitType, inheritedConstantNames, inheritedConstantMuliplesNames, includedUnitPlurals)
    {
        IncludedUnitNames = includedUnitNames;
    }
}
