namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal record class SharpMeasuresVectorGroupResolutionContext : SimpleProcessingContext, ISharpMeasuresVectorGroupResolutionContext
{
    public IRawUnitPopulation UnitPopulation { get; }
    public IRawScalarPopulation ScalarPopulation { get; }
    public IUnresolvedVectorPopulationWithData VectorPopulation { get; }

    public SharpMeasuresVectorGroupResolutionContext(DefinedType type, IRawUnitPopulation unitPopulation, IRawScalarPopulation scalarPopulation, IUnresolvedVectorPopulationWithData vectorPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
