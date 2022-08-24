namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Documentation;

internal record class DataModel
{
    public UnitType Unit { get; }

    public IUnitPopulation UnitPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(UnitType unit, IUnitPopulation unitPopulation)
    {
        Unit = unit;

        UnitPopulation = unitPopulation;
    }
}
