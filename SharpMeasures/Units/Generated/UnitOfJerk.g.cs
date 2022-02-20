#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Jerk"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfJerk"/> exists as static properties, and from these custom <see cref="UnitOfJerk"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfJerk"/> can also be derived from
/// other units using the static <see cref="From(UnitOfAcceleration, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfJerk :
    IComparable<UnitOfJerk>
{
    /// <summary>Derives a <see cref="UnitOfJerk"/> according to { <paramref name="unitOfAcceleration"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfAcceleration">A <see cref="UnitOfJerk"/> is derived from division of this <see cref="UnitOfAcceleration"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfJerk"/> is derived from division of <paramref name="unitOfAcceleration"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfJerk From(UnitOfAcceleration unitOfAcceleration, UnitOfTime unitOfTime) => new(Jerk.From(unitOfAcceleration.Acceleration, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Jerk"/>, derived according to { <see cref="UnitOfAcceleration.MetrePerSecondSquared"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [m/s³] or [m∙s⁻³].</summary>
    public static UnitOfJerk MetrePerSecondCubed { get; } = From(UnitOfAcceleration.MetrePerSecondSquared, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Jerk"/> according to { <see cref="UnitOfAcceleration.FootPerSecondSquared"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [ft/s³] or [ft∙s⁻³].</summary>
    public static UnitOfJerk FootPerSecondCubed { get; } = From(UnitOfAcceleration.FootPerSecondSquared, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.Jerk"/> that the <see cref="UnitOfJerk"/> represents.</summary>
    public Jerk Jerk { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfJerk"/>, representing the <see cref="Quantities.Jerk"/> <paramref name="jerk"/>.</summary>
    /// <param name="jerk">The <see cref="Quantities.Jerk"/> that the new <see cref="UnitOfJerk"/> represents.</param>
    private UnitOfJerk(Jerk jerk)
    {
        Jerk = jerk;
    }

    /// <summary>Derives a new <see cref="UnitOfJerk"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfJerk"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfJerk WithPrefix(MetricPrefix prefix) => new(Jerk * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfJerk"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfJerk"/> is scaled by this value.</param>
    public UnitOfJerk ScaledBy(Scalar scale) => new(Jerk * scale);
    /// <summary>Derives a new <see cref="UnitOfJerk"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfJerk"/> is scaled by this value.</param>
    public UnitOfJerk ScaledBy(double scale) => new(Jerk * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfJerk other) => Jerk.CompareTo(other.Jerk);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Jerk"/>.</summary>
    public override string ToString() => $"{GetType()}: {Jerk}";

    /// <summary>Determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Jerk"/> represented by this <see cref="UnitOfJerk"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfJerk"/>.</param>
    public static bool operator <(UnitOfJerk x, UnitOfJerk y) => x.Jerk < y.Jerk;
    /// <summary>Determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Jerk"/> represented by this <see cref="UnitOfJerk"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfJerk"/>.</param>
    public static bool operator >(UnitOfJerk x, UnitOfJerk y) => x.Jerk > y.Jerk;
    /// <summary>Determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Jerk"/> represented by this <see cref="UnitOfJerk"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfJerk"/>.</param>
    public static bool operator <=(UnitOfJerk x, UnitOfJerk y) => x.Jerk <= y.Jerk;
    /// <summary>Determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Jerk"/> represented by this <see cref="UnitOfJerk"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Jerk"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfJerk"/>.</param>
    public static bool operator >=(UnitOfJerk x, UnitOfJerk y) => x.Jerk >= y.Jerk;
}
