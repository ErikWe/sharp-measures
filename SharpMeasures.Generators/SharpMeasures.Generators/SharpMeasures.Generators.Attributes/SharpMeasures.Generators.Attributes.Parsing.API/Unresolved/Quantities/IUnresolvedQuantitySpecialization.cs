﻿namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedQuantitySpecialization : IUnresolvedQuantity
{
    public abstract NamedType OriginalQuantity { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }
}