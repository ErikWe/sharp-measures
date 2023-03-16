namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

public sealed class QuantityOperationValidationContext : IQuantityOperationValidationContext
{
    public DefinedType Type { get; }

    public QuantityType QuantityType { get; }
    public IReadOnlyList<int> VectorDimensions { get; }

    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public QuantityOperationValidationContext(DefinedType type, QuantityType quantityType, IReadOnlyList<int> vectorDimensions, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        Type = type;

        QuantityType = quantityType;
        VectorDimensions = vectorDimensions;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
