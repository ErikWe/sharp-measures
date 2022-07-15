namespace SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedQuantityType : ISharpMeasuresObjectType
{
    new public abstract IUnresolvedQuantity Definition { get; }

    public abstract IReadOnlyList<IUnresolvedDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IUnresolvedQuantityConstant> Constants { get; }
    public abstract IReadOnlyList<IUnresolvedConvertibleQuantity> ConvertibleQuantities { get; }

    public abstract IReadOnlyList<IUnresolvedUnitList> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitList> UnitExclusions { get; }

    public IReadOnlyDictionary<string, IUnresolvedQuantityConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IUnresolvedQuantityConstant> ConstantsByMultiplesName { get; }
}
