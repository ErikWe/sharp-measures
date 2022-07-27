namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IQuantity : ISharpMeasuresObject
{
    public abstract IUnresolvedUnitType Unit { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }
    public abstract IUnresolvedQuantityType Difference { get; }

    public abstract IUnresolvedUnitInstance? DefaultUnit { get; }
    public abstract string? DefaultUnitSymbol { get; }
}
