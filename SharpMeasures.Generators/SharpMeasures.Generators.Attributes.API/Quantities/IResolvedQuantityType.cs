namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedQuantityType
{
    public abstract DefinedType Type { get; }
    public abstract MinimalLocation TypeLocation { get; }

    public abstract NamedType Unit { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }
    public abstract NamedType? Difference { get; }

    public abstract string? DefaultUnitInstanceName { get; }
    public abstract string? DefaultUnitInstanceSymbol { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public abstract IReadOnlyList<string> IncludedUnitInstanceNames { get; }
}
