namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal abstract record class ADataModel<TScalarType>
    where TScalarType : IScalarType
{
    public TScalarType Scalar { get; }
    public InheritanceData Inheritance { get; }

    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    protected ADataModel(TScalarType scalar, InheritanceData inheritance, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation,
        IVectorPopulation vectorPopulation)
    {
        Scalar = scalar;
        Inheritance = inheritance;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
