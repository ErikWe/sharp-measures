namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Units;

internal record class IndividualVectorDataModel : ADataModel<IIndividualVectorType, IIndividualVectorDocumentationStrategy>
{
    public IndividualVectorDataModel(IIndividualVectorType vector, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
        : base(vector, unitPopulation, scalarPopulation, vectorPopulation, EmptyDocumentation.Instance) { }
}
