namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;

internal sealed record class VectorOperationValidationContext : IVectorOperationValidationContext
{
    public DefinedType Type { get; }
    
    public IReadOnlyList<int> Dimensions { get; }

    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public VectorOperationValidationContext(DefinedType type, IReadOnlyList<int> dimensions, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        Type = type;

        Dimensions = dimensions;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
