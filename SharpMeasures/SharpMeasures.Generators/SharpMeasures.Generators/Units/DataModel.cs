namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing;

internal record class DataModel
{
    public ParsedUnit Unit { get; }
    public ScalarInterface Quantity { get; }

    public NamedTypePopulation<UnitInterface> UnitPopulation { get; }

    public DocumentationFile Documentation { get; init; }

    public DataModel(ParsedUnit unit, ScalarInterface quantity, NamedTypePopulation<UnitInterface> unitPopulation)
    {
        Unit = unit;
        Quantity = quantity;

        UnitPopulation = unitPopulation;
    }
}
