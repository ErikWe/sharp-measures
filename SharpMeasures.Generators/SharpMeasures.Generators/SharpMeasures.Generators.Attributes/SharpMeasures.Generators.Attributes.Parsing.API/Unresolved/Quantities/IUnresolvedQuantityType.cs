namespace SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedQuantityType : ISharpMeasuresObjectType
{
    new public abstract IUnresolvedQuantity Definition { get; }

    public abstract IReadOnlyList<IUnresolvedDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IUnresolvedConvertibleQuantity> Conversions { get; }

    public abstract IReadOnlyList<IUnresolvedUnitList> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitList> UnitExclusions { get; }
}
