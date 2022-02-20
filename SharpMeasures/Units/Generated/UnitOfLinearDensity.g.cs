#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.LinearDensity"/>.</summary>
/// <remarks>Common <see cref="UnitOfLinearDensity"/> exists as static properties, and from these custom <see cref="UnitOfLinearDensity"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfLinearDensity"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfLength)"/>.</remarks>
public readonly record struct UnitOfLinearDensity :
    IComparable<UnitOfLinearDensity>
{
    /// <summary>Derives a <see cref="UnitOfLinearDensity"/> according to { <paramref name="unitOfMass"/> / <paramref name="unitOfLength"/> }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfLinearDensity"/> is derived from division of this <see cref="UnitOfMass"/> by <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">A <see cref="UnitOfLinearDensity"/> is derived from division of <paramref name="unitOfMass"/> by this <see cref="UnitOfLength"/>.</param>
    public static UnitOfLinearDensity From(UnitOfMass unitOfMass, UnitOfLength unitOfLength) => new(LinearDensity.From(unitOfMass.Mass, unitOfLength.Length));

    /// <summary>The SI unit of <see cref="Quantities.LinearDensity"/>, derived according to { <see cref="UnitOfMass.Kilogram"/> / <see cref="UnitOfLength.Metre"/> }.
    /// Usually written as [kg/m] or [kg∙m⁻¹].</summary>
    public static UnitOfLinearDensity KilogramPerMetre { get; } = From(UnitOfMass.Kilogram, UnitOfLength.Metre);

    /// <summary>The <see cref="Quantities.LinearDensity"/> that the <see cref="UnitOfLinearDensity"/> represents.</summary>
    public LinearDensity LinearDensity { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfLinearDensity"/>, representing the <see cref="Quantities.LinearDensity"/> <paramref name="linearDensity"/>.</summary>
    /// <param name="linearDensity">The <see cref="Quantities.LinearDensity"/> that the new <see cref="UnitOfLinearDensity"/> represents.</param>
    private UnitOfLinearDensity(LinearDensity linearDensity)
    {
        LinearDensity = linearDensity;
    }

    /// <summary>Derives a new <see cref="UnitOfLinearDensity"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfLinearDensity"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfLinearDensity WithPrefix(MetricPrefix prefix) => new(LinearDensity * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfLinearDensity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfLinearDensity"/> is scaled by this value.</param>
    public UnitOfLinearDensity ScaledBy(Scalar scale) => new(LinearDensity * scale);
    /// <summary>Derives a new <see cref="UnitOfLinearDensity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfLinearDensity"/> is scaled by this value.</param>
    public UnitOfLinearDensity ScaledBy(double scale) => new(LinearDensity * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfLinearDensity other) => LinearDensity.CompareTo(other.LinearDensity);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.LinearDensity"/>.</summary>
    public override string ToString() => $"{GetType()}: {LinearDensity}";

    /// <summary>Determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by this <see cref="UnitOfLinearDensity"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfLinearDensity"/>.</param>
    public static bool operator <(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.LinearDensity < y.LinearDensity;
    /// <summary>Determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by this <see cref="UnitOfLinearDensity"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfLinearDensity"/>.</param>
    public static bool operator >(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.LinearDensity > y.LinearDensity;
    /// <summary>Determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by this <see cref="UnitOfLinearDensity"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfLinearDensity"/>.</param>
    public static bool operator <=(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.LinearDensity <= y.LinearDensity;
    /// <summary>Determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by this <see cref="UnitOfLinearDensity"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.LinearDensity"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfLinearDensity"/>.</param>
    public static bool operator >=(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.LinearDensity >= y.LinearDensity;
}
