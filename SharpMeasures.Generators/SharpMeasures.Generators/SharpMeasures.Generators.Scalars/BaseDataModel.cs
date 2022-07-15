namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal record class BaseDataModel : ADataModel<BaseScalarType>
{
    public BaseDataModel(BaseScalarType scalar, InheritanceData inheritance, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation,
        IVectorPopulation vectorPopulation)
        : base(scalar, inheritance, unitPopulation, scalarPopulation, vectorPopulation) { }
}
