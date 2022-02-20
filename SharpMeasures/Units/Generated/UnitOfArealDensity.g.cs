#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.ArealDensity"/>.</summary>
/// <remarks>Common <see cref="UnitOfArealDensity"/> exists as static properties, and from these custom <see cref="UnitOfArealDensity"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfArealDensity"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfArea)"/></remarks>
public readonly record struct UnitOfArealDensity :
    IComparable<UnitOfArealDensity>
{
    /// <summary>Derives a <see cref="UnitOfArealDensity"/> according to { <paramref name="unitOfMass"/> / <paramref name="unitOfArea"/> }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfArealDensity"/> is derived from division of this <see cref="UnitOfMass"/> by <paramref name="unitOfArea"/>.</param>
    /// <param name="unitOfArea">A <see cref="UnitOfArealDensity"/> is derived from division of <paramref name="unitOfMass"/> by this <see cref="UnitOfArea"/>.</param>
    public static UnitOfArealDensity From(UnitOfMass unitOfMass, UnitOfArea unitOfArea) => new(ArealDensity.From(unitOfMass.Mass, unitOfArea.Area));

    /// <summary>The SI unit of <see cref="Quantities.ArealDensity"/>, derived according to { <see cref="UnitOfMass.Kilogram"/> / <see cref="UnitOfArea.SquareMetre"/> }.
    /// Usually written as [kg/m²] or [kg∙m⁻²].</summary>
    public static UnitOfArealDensity KilogramPerSquareMetre { get; } = From(UnitOfMass.Kilogram, UnitOfArea.SquareMetre);

    /// <summary>The <see cref="Quantities.ArealDensity"/> that the <see cref="UnitOfArealDensity"/> represents.</summary>
    public ArealDensity ArealDensity { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfArealDensity"/>, representing the <see cref="Quantities.ArealDensity"/> <paramref name="arealDensity"/>.</summary>
    /// <param name="arealDensity">The <see cref="Quantities.ArealDensity"/> that the new <see cref="UnitOfArealDensity"/> represents.</param>
    private UnitOfArealDensity(ArealDensity arealDensity)
    {
        ArealDensity = arealDensity;
    }

    /// <summary>Derives a new <see cref="UnitOfArealDensity"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfArealDensity"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfArealDensity WithPrefix(MetricPrefix prefix) => new(ArealDensity * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfArealDensity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfArealDensity"/> is scaled by this value.</param>
    public UnitOfArealDensity ScaledBy(Scalar scale) => new(ArealDensity * scale);
    /// <summary>Derives a new <see cref="UnitOfArealDensity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfArealDensity"/> is scaled by this value.</param>
    public UnitOfArealDensity ScaledBy(double scale) => new(ArealDensity * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfArealDensity other) => ArealDensity.CompareTo(other.ArealDensity);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.ArealDensity"/>.</summary>
    public override string ToString() => $"{GetType()}: {ArealDensity}";

    /// <summary>Determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by this <see cref="UnitOfArealDensity"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfArealDensity"/>.</param>
    public static bool operator <(UnitOfArealDensity x, UnitOfArealDensity y) => x.ArealDensity < y.ArealDensity;
    /// <summary>Determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by this <see cref="UnitOfArealDensity"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfArealDensity"/>.</param>
    public static bool operator >(UnitOfArealDensity x, UnitOfArealDensity y) => x.ArealDensity > y.ArealDensity;
    /// <summary>Determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by this <see cref="UnitOfArealDensity"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfArealDensity"/>.</param>
    public static bool operator <=(UnitOfArealDensity x, UnitOfArealDensity y) => x.ArealDensity <= y.ArealDensity;
    /// <summary>Determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by this <see cref="UnitOfArealDensity"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.ArealDensity"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfArealDensity"/>.</param>
    public static bool operator >=(UnitOfArealDensity x, UnitOfArealDensity y) => x.ArealDensity >= y.ArealDensity;
}
