namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IQuantityType : ISharpMeasuresObjectType
{
    new public abstract IQuantity Definition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public abstract IReadOnlyList<IUnitInstanceInclusionList> UnitInstanceInclusions { get; }
    public abstract IReadOnlyList<IUnitInstanceList> UnitInstanceExclusions { get; }
}
