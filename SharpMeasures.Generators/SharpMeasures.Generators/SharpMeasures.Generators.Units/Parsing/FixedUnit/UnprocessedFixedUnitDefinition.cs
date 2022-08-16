namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnprocessedFixedUnitDefinition : AUnprocessedUnitDefinition<UnprocessedFixedUnitDefinition, FixedUnitLocations>
{
    public static UnprocessedFixedUnitDefinition Empty { get; } = new();

    protected override UnprocessedFixedUnitDefinition Definition => this;

    private UnprocessedFixedUnitDefinition() : base(FixedUnitLocations.Empty) { }
}
