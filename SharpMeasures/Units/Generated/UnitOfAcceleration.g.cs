#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Acceleration"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfAcceleration"/> exists as static properties, and from these custom <see cref="UnitOfAcceleration"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfAcceleration"/> can also be derived from
/// other units using the static <see cref="From(UnitOfVelocity, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfAcceleration :
    IComparable<UnitOfAcceleration>
{
    /// <summary>Derives a <see cref="UnitOfAcceleration"/> according to { <paramref name="unitOfVelocity"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfVelocity">A <see cref="UnitOfAcceleration"/> is derived from division of this <see cref="UnitOfVelocity"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfAcceleration"/> is derived from division of <paramref name="unitOfVelocity"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfAcceleration From(UnitOfVelocity unitOfVelocity, UnitOfTime unitOfTime) => new(Acceleration.From(unitOfVelocity.Speed, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Acceleration"/>, derived according to { <see cref="UnitOfVelocity.MetrePerSecond"/> /
    /// <see cref="UnitOfTime.Second"/> }. Usually written as [m/s²] or [m∙s⁻²].</summary>
    public static UnitOfAcceleration MetrePerSecondSquared { get; } = From(UnitOfVelocity.MetrePerSecond, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Acceleration"/> according to { <see cref="UnitOfVelocity.FootPerSecond"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [ft/s²] or [ft∙s⁻²].</summary>
    public static UnitOfAcceleration FootPerSecondSquared { get; } = From(UnitOfVelocity.FootPerSecond, UnitOfTime.Second);

    /// <summary>A constant <see cref="Quantities.Acceleration"/>, representing the standard gravity on Earth - with value { 9.80665 ∙ <see cref="MetrePerSecondSquared"/> }.
    /// Usually written as [g].</summary>
    public static UnitOfAcceleration StandardGravity { get; } = MetrePerSecondSquared.ScaledBy(9.80665);

    /// <summary>The <see cref="Quantities.Acceleration"/> that the <see cref="UnitOfAcceleration"/> represents.</summary>
    public Acceleration Acceleration { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfAcceleration"/>, representing the <see cref="Quantities.Acceleration"/> <paramref name="acceleration"/>.</summary>
    /// <param name="acceleration">The <see cref="Quantities.Acceleration"/> that the new <see cref="UnitOfAcceleration"/> represents.</param>
    private UnitOfAcceleration(Acceleration acceleration)
    {
        Acceleration = acceleration;
    }

    /// <summary>Derives a new <see cref="UnitOfAcceleration"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfAcceleration"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfAcceleration WithPrefix(MetricPrefix prefix) => new(Acceleration * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfAcceleration"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAcceleration"/> is scaled by this value.</param>
    public UnitOfAcceleration ScaledBy(Scalar scale) => new(Acceleration * scale);
    /// <summary>Derives a new <see cref="UnitOfAcceleration"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAcceleration"/> is scaled by this value.</param>
    public UnitOfAcceleration ScaledBy(double scale) => new(Acceleration * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfAcceleration other) => Acceleration.CompareTo(other.Acceleration);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Acceleration"/>.</summary>
    public override string ToString() => $"{GetType()}: {Acceleration}";

    /// <summary>Determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Acceleration"/> represented by this <see cref="UnitOfAcceleration"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfAcceleration"/>.</param>
    public static bool operator <(UnitOfAcceleration x, UnitOfAcceleration y) => x.Acceleration < y.Acceleration;
    /// <summary>Determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Acceleration"/> represented by this <see cref="UnitOfAcceleration"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfAcceleration"/>.</param>
    public static bool operator >(UnitOfAcceleration x, UnitOfAcceleration y) => x.Acceleration > y.Acceleration;
    /// <summary>Determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Acceleration"/> represented by this <see cref="UnitOfAcceleration"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfAcceleration"/>.</param>
    public static bool operator <=(UnitOfAcceleration x, UnitOfAcceleration y) => x.Acceleration <= y.Acceleration;
    /// <summary>Determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Acceleration"/> represented by this <see cref="UnitOfAcceleration"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Acceleration"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfAcceleration"/>.</param>
    public static bool operator >=(UnitOfAcceleration x, UnitOfAcceleration y) => x.Acceleration >= y.Acceleration;
}
