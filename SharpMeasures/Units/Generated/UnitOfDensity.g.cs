#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Density"/>.</summary>
/// <remarks>Common <see cref="UnitOfDensity"/> exists as static properties, and from these custom <see cref="UnitOfDensity"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfDensity"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfVolume)"/></remarks>
public readonly record struct UnitOfDensity :
    IComparable<UnitOfDensity>
{
    /// <summary>Derives a <see cref="UnitOfDensity"/> according to { <paramref name="unitOfMass"/> / <paramref name="unitOfVolume"/> }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfDensity"/> is derived from division of this <see cref="UnitOfMass"/> by <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">A <see cref="UnitOfDensity"/> is derived from division of <paramref name="unitOfMass"/> by this <see cref="UnitOfVolume"/>.</param>
    public static UnitOfDensity From(UnitOfMass unitOfMass, UnitOfVolume unitOfVolume) => new(Density.From(unitOfMass.Mass, unitOfVolume.Volume));

    /// <summary>The SI unit of <see cref="Quantities.Density"/>, derived according to { <see cref="UnitOfMass.Kilogram"/> / <see cref="UnitOfVolume.CubicMetre"/> }.
    /// Usually written as [kg/m³] or [kg∙m⁻³].</summary>
    public static UnitOfDensity KilogramPerCubicMetre { get; } = From(UnitOfMass.Kilogram, UnitOfVolume.CubicMetre);

    /// <summary>The <see cref="Quantities.Density"/> that the <see cref="UnitOfDensity"/> represents.</summary>
    public Density Density { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfDensity"/>, representing the <see cref="Quantities.Density"/> <paramref name="density"/>.</summary>
    /// <param name="density">The <see cref="Quantities.Density"/> that the new <see cref="UnitOfDensity"/> represents.</param>
    private UnitOfDensity(Density density)
    {
        Density = density;
    }

    /// <summary>Derives a new <see cref="UnitOfDensity"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfDensity"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfDensity WithPrefix(MetricPrefix prefix) => new(Density * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfDensity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfDensity"/> is scaled by this value.</param>
    public UnitOfDensity ScaledBy(Scalar scale) => new(Density * scale);
    /// <summary>Derives a new <see cref="UnitOfDensity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfDensity"/> is scaled by this value.</param>
    public UnitOfDensity ScaledBy(double scale) => new(Density * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfDensity other) => Density.CompareTo(other.Density);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Density"/>.</summary>
    public override string ToString() => $"{GetType()}: {Density}";

    /// <summary>Determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Density"/> represented by this <see cref="UnitOfDensity"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfDensity"/>.</param>
    public static bool operator <(UnitOfDensity x, UnitOfDensity y) => x.Density < y.Density;
    /// <summary>Determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Density"/> represented by this <see cref="UnitOfDensity"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfDensity"/>.</param>
    public static bool operator >(UnitOfDensity x, UnitOfDensity y) => x.Density > y.Density;
    /// <summary>Determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Density"/> represented by this <see cref="UnitOfDensity"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfDensity"/>.</param>
    public static bool operator <=(UnitOfDensity x, UnitOfDensity y) => x.Density <= y.Density;
    /// <summary>Determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Density"/> represented by this <see cref="UnitOfDensity"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Density"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfDensity"/>.</param>
    public static bool operator >=(UnitOfDensity x, UnitOfDensity y) => x.Density >= y.Density;
}
