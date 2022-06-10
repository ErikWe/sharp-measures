namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing;

internal record class DataModel
{
    public ParsedUnit Unit { get; }
    public ScalarInterface Quantity { get; }

    public UnitPopulation UnitPopulation { get; }

    public DocumentationFile Documentation { get; init; } = DocumentationFile.Empty;

    public DataModel(ParsedUnit unit, ScalarInterface quantity, UnitPopulation unitPopulation)
    {
        Unit = unit;
        Quantity = quantity;

        UnitPopulation = unitPopulation;
    }
}
