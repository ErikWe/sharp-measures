namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Units;

public record class UnitListResolutionContext : IUnitListResolutionContext
{
    public DefinedType Type { get; }

    public IUnresolvedUnitType Unit { get; }

    public UnitListResolutionContext(DefinedType type, IUnresolvedUnitType unit)
    {
        Type = type;

        Unit = unit;
    }
}
