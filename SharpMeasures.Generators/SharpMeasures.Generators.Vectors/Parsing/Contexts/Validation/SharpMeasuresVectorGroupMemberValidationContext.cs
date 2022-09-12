namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal record class SharpMeasuresVectorGroupMemberValidationContext : SimpleProcessingContext, ISharpMeasuresVectorGroupMemberValidationContext
{
    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulationWithData VectorPopulation { get; }

    public SharpMeasuresVectorGroupMemberValidationContext(DefinedType type, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
