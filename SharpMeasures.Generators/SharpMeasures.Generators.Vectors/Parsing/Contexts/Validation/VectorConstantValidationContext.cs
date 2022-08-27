namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class VectorConstantValidationContext : QuantityConstantValidationContext, IVectorConstantValidationContext
{
    public int Dimension { get; }

    public VectorConstantValidationContext(DefinedType type, int dimension, IUnitType unitType, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMuliplesNames, HashSet<string> includedUnitPlurals)
        : base(type, unitType, inheritedConstantNames, inheritedConstantMuliplesNames, includedUnitPlurals)
    {
        Dimension = dimension;
    }
}
