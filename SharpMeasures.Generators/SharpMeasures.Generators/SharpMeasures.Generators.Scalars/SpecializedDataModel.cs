namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal record class SpecializedDataModel : ADataModel<SpecializedScalarType>
{
    public SpecializedDataModel(SpecializedScalarType scalar, InheritanceData inheritance, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation,
        IVectorPopulation vectorPopulation)
        : base(scalar, inheritance, unitPopulation, scalarPopulation, vectorPopulation) { }
}
