namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IQuantityType : ISharpMeasuresObjectType
{
    new public abstract IQuantity Definition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IQuantityConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleQuantity> ConvertibleQuantities { get; }

    public abstract IReadOnlyList<IUnitList> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnitList> UnitExclusions { get; }

    public IReadOnlyDictionary<string, IQuantityConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IQuantityConstant> ConstantsByMultiplesName { get; }
}
