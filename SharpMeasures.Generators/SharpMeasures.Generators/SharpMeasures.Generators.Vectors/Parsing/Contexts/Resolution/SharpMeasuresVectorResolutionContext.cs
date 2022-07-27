namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal record class SharpMeasuresVectorResolutionContext : SimpleProcessingContext, ISharpMeasuresVectorResolutionContext
{
    public IUnresolvedUnitPopulation UnitPopulation { get; }
    public IUnresolvedScalarPopulation ScalarPopulation { get; }
    public IUnresolvedVectorPopulation VectorPopulation { get; }

    public SharpMeasuresVectorResolutionContext(DefinedType type, IUnresolvedUnitPopulation unitPopulation, IUnresolvedScalarPopulation scalarPopulation,
        IUnresolvedVectorPopulation vectorPopulation) : base(type)
    {
        ScalarPopulation = scalarPopulation;
        UnitPopulation = unitPopulation;
        VectorPopulation = vectorPopulation;
    }
}
