namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IQuantity : ISharpMeasuresObject
{
    public abstract IRawUnitType Unit { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }
    public abstract IRawQuantityType Difference { get; }

    public abstract IRawUnitInstance? DefaultUnit { get; }
    public abstract string? DefaultUnitSymbol { get; }
}
