namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Groups;

internal record class VectorGroupDataModel : ADataModel<IVectorGroupType, IVectorGroupDocumentationStrategy>
{
    public VectorGroupDataModel(IVectorGroupType vector, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
        : base(vector, unitPopulation, scalarPopulation, vectorPopulation, EmptyDocumentation.Instance) { }
}
