namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.Refinement.GeneratedUnit;

internal record class DataModel
{
    public RefinedGeneratedUnitDefinition UnitDefinition { get; }
    public ParsedUnit UnitData { get; }

    public UnitPopulation UnitPopulation { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(RefinedGeneratedUnitDefinition unitDefinition, ParsedUnit unitData, UnitPopulation unitPopulation)
    {
        UnitDefinition = unitDefinition;
        UnitData = unitData;

        UnitPopulation = unitPopulation;
    }
}
