namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

public interface IQuantityType : ISharpMeasuresObjectType
{
    new public abstract IQuantity Definition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public abstract IReadOnlyList<IRawUnitInstance> IncludedUnits { get; }
}
