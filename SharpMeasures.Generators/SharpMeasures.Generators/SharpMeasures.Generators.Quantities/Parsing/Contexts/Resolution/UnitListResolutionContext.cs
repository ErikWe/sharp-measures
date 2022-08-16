namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Units;

public record class UnitListResolutionContext : IUnitListResolutionContext
{
    public DefinedType Type { get; }

    public IRawUnitType Unit { get; }

    public UnitListResolutionContext(DefinedType type, IRawUnitType unit)
    {
        Type = type;

        Unit = unit;
    }
}
