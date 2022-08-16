namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Vectors;

internal record class SpecializedSharpMeasuresScalarResolutionContext : SimpleProcessingContext, ISpecializedSharpMeasuresScalarResolutionContext
{
    public IRawUnitPopulation UnitPopulation { get; }
    public IUnresolvedScalarPopulationWithData ScalarPopulation { get; }
    public IRawVectorPopulation VectorPopulation { get; }

    public SpecializedSharpMeasuresScalarResolutionContext(DefinedType type, IRawUnitPopulation unitPopulation, IUnresolvedScalarPopulationWithData scalarPopulation, IRawVectorPopulation vectorPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
