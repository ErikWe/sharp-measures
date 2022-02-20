#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.SpeedSquared"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfVelocitySquared"/> exists as static properties, and from these custom <see cref="UnitOfVelocitySquared"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfVelocitySquared"/> can also be derived from
/// other units using the static <see cref="From(UnitOfVelocity)"/>, or an overload.</remarks>
public readonly record struct UnitOfVelocitySquared :
    IComparable<UnitOfVelocitySquared>
{
    /// <summary>Derives a <see cref="UnitOfVelocitySquared"/> according to { <paramref name="unitOfVelocity"/>² }.</summary>
    /// <param name="unitOfVelocity">A <see cref="UnitOfVelocitySquared"/> is derived from squaring this <see cref="UnitOfVelocity"/>.</param>
    public static UnitOfVelocitySquared From(UnitOfVelocity unitOfVelocity) => new(SpeedSquared.From(unitOfVelocity.Speed));
    /// <summary>Derives a <see cref="UnitOfVelocitySquared"/> according to { <paramref name="unitOfVelocity1"/> ∙ <paramref name="unitOfVelocity2"/> }.</summary>
    /// <param name="unitOfVelocity1">A <see cref="UnitOfVelocitySquared"/> is derived from multiplication of this <see cref="UnitOfVelocity"/> by <paramref name="unitOfVelocity2"/>.</param>
    /// <param name="unitOfVelocity2">A <see cref="UnitOfVelocitySquared"/> is derived from multiplication of this <see cref="UnitOfVelocity"/> by <paramref name="unitOfVelocity1"/>.</param>
    public static UnitOfVelocitySquared From(UnitOfVelocity unitOfVelocity1, UnitOfVelocity unitOfVelocity2) 
    	=> new(SpeedSquared.From(unitOfVelocity1.Speed, unitOfVelocity2.Speed));

    /// <summary>The SI unit of <see cref="Quantities.SpeedSquared"/>, derived according to { <see cref="UnitOfVelocity.MetrePerSecond"/>² }.
    /// Usually written as [m²/s²] or [m²∙s⁻²].</summary>
    public static UnitOfVelocitySquared SquareMetrePerSecondSquared { get; } = From(UnitOfVelocity.MetrePerSecond);

    /// <summary>The <see cref="Quantities.SpeedSquared"/> that the <see cref="UnitOfVelocitySquared"/> represents.</summary>
    public SpeedSquared SpeedSquared { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfVelocitySquared"/>, representing the <see cref="Quantities.SpeedSquared"/> <paramref name="speedSquared"/>.</summary>
    /// <param name="speedSquared">The <see cref="Quantities.SpeedSquared"/> that the new <see cref="UnitOfVelocitySquared"/> represents.</param>
    private UnitOfVelocitySquared(SpeedSquared speedSquared)
    {
        SpeedSquared = speedSquared;
    }

    /// <summary>Derives a new <see cref="UnitOfVelocitySquared"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfVelocitySquared"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfVelocitySquared WithPrefix(MetricPrefix prefix) => new(SpeedSquared * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfVelocitySquared"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVelocitySquared"/> is scaled by this value.</param>
    public UnitOfVelocitySquared ScaledBy(Scalar scale) => new(SpeedSquared * scale);
    /// <summary>Derives a new <see cref="UnitOfVelocitySquared"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVelocitySquared"/> is scaled by this value.</param>
    public UnitOfVelocitySquared ScaledBy(double scale) => new(SpeedSquared * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfVelocitySquared other) => SpeedSquared.CompareTo(other.SpeedSquared);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.SpeedSquared"/>.</summary>
    public override string ToString() => $"{GetType()}: {SpeedSquared}";

    /// <summary>Determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by this <see cref="UnitOfVelocitySquared"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfVelocitySquared"/>.</param>
    public static bool operator <(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.SpeedSquared < y.SpeedSquared;
    /// <summary>Determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by this <see cref="UnitOfVelocitySquared"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfVelocitySquared"/>.</param>
    public static bool operator >(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.SpeedSquared > y.SpeedSquared;
    /// <summary>Determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by this <see cref="UnitOfVelocitySquared"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfVelocitySquared"/>.</param>
    public static bool operator <=(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.SpeedSquared <= y.SpeedSquared;
    /// <summary>Determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by this <see cref="UnitOfVelocitySquared"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpeedSquared"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfVelocitySquared"/>.</param>
    public static bool operator >=(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.SpeedSquared >= y.SpeedSquared;
}
