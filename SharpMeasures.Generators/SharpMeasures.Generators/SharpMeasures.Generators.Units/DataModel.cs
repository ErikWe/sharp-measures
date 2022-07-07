namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Documentation;

internal record class DataModel
{
    public UnitType UnitData { get; }

    public IUnitPopulation UnitPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(UnitType unitData, IUnitPopulation unitPopulation)
    {
        UnitData = unitData;

        UnitPopulation = unitPopulation;
    }
}
