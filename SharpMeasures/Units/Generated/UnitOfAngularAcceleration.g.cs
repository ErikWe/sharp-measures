#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.AngularAcceleration"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfAngularAcceleration"/> exists as static properties, and from these custom <see cref="UnitOfAngularAcceleration"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfAngularAcceleration"/> can also be derived from
/// other units using the static <see cref="From(UnitOfAngularVelocity, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfAngularAcceleration :
    IComparable<UnitOfAngularAcceleration>
{
    /// <summary>Derives a <see cref="UnitOfAngularAcceleration"/> according to { <paramref name="unitOfAngularVelocity"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfAngularVelocity">A <see cref="UnitOfAngularAcceleration"/> is derived from division of this <see cref="UnitOfAngularVelocity"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfAngularAcceleration"/> is derived from division of <paramref name="unitOfAngularVelocity"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfAngularAcceleration From(UnitOfAngularVelocity unitOfAngularVelocity, UnitOfTime unitOfTime) 
    	=> new(AngularAcceleration.From(unitOfAngularVelocity.AngularSpeed, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.AngularAcceleration"/>, derived according to {
    /// <see cref="UnitOfAngularVelocity.RadianPerSecond"/> / <see cref="UnitOfTime.Second"/> }. Usually written as [rad/s²] or [rad∙s⁻²].</summary>
    public static UnitOfAngularAcceleration RadianPerSecondSquared { get; } = From(UnitOfAngularVelocity.RadianPerSecond, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.AngularAcceleration"/> that the <see cref="UnitOfAngularAcceleration"/> represents.</summary>
    public AngularAcceleration AngularAcceleration { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfAngularAcceleration"/>, representing the <see cref="Quantities.AngularAcceleration"/> <paramref name="angularAcceleration"/>.</summary>
    /// <param name="angularAcceleration">The <see cref="Quantities.AngularAcceleration"/> that the new <see cref="UnitOfAngularAcceleration"/> represents.</param>
    private UnitOfAngularAcceleration(AngularAcceleration angularAcceleration)
    {
        AngularAcceleration = angularAcceleration;
    }

    /// <summary>Derives a new <see cref="UnitOfAngularAcceleration"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfAngularAcceleration"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfAngularAcceleration WithPrefix(MetricPrefix prefix) => new(AngularAcceleration * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfAngularAcceleration"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngularAcceleration"/> is scaled by this value.</param>
    public UnitOfAngularAcceleration ScaledBy(Scalar scale) => new(AngularAcceleration * scale);
    /// <summary>Derives a new <see cref="UnitOfAngularAcceleration"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngularAcceleration"/> is scaled by this value.</param>
    public UnitOfAngularAcceleration ScaledBy(double scale) => new(AngularAcceleration * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfAngularAcceleration other) => AngularAcceleration.CompareTo(other.AngularAcceleration);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.AngularAcceleration"/>.</summary>
    public override string ToString() => $"{GetType()}: {AngularAcceleration}";

    /// <summary>Determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by this <see cref="UnitOfAngularAcceleration"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfAngularAcceleration"/>.</param>
    public static bool operator <(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.AngularAcceleration < y.AngularAcceleration;
    /// <summary>Determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by this <see cref="UnitOfAngularAcceleration"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfAngularAcceleration"/>.</param>
    public static bool operator >(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.AngularAcceleration > y.AngularAcceleration;
    /// <summary>Determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by this <see cref="UnitOfAngularAcceleration"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfAngularAcceleration"/>.</param>
    public static bool operator <=(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.AngularAcceleration <= y.AngularAcceleration;
    /// <summary>Determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by this <see cref="UnitOfAngularAcceleration"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularAcceleration"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfAngularAcceleration"/>.</param>
    public static bool operator >=(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.AngularAcceleration >= y.AngularAcceleration;
}
