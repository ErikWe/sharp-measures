namespace SharpMeasures.Generators.Units;

internal sealed record class DataModel
{
    public UnitType Unit { get; }

    public IUnitPopulation UnitPopulation { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(UnitType unit, IUnitPopulation unitPopulation, SourceBuildingContext sourceBuildingContext)
    {
        Unit = unit;

        UnitPopulation = unitPopulation;

        SourceBuildingContext = sourceBuildingContext;
    }
}
