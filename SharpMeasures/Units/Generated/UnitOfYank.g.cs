#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Yank"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfYank"/> exists as static properties, and from these custom <see cref="UnitOfYank"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfYank"/> can also be derived from
/// other units using the static <see cref="From(UnitOfForce, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfYank :
    IComparable<UnitOfYank>
{
    /// <summary>Derives a <see cref="UnitOfYank"/> according to { <paramref name="unitOfForce"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfForce">A <see cref="UnitOfYank"/> is derived from division of this <see cref="UnitOfForce"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfYank"/> is derived from division of <paramref name="unitOfForce"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfYank From(UnitOfForce unitOfForce, UnitOfTime unitOfTime) => new(Yank.From(unitOfForce.Force, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Yank"/>, derived according to { <see cref="UnitOfForce.Newton"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [N/s] or [N∙s⁻¹].</summary>
    public static UnitOfYank NewtonPerSecond { get; } = From(UnitOfForce.Newton, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.Yank"/> that the <see cref="UnitOfYank"/> represents.</summary>
    public Yank Yank { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfYank"/>, representing the <see cref="Quantities.Yank"/> <paramref name="yank"/>.</summary>
    /// <param name="yank">The <see cref="Quantities.Yank"/> that the new <see cref="UnitOfYank"/> represents.</param>
    private UnitOfYank(Yank yank)
    {
        Yank = yank;
    }

    /// <summary>Derives a new <see cref="UnitOfYank"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfYank"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfYank WithPrefix(MetricPrefix prefix) => new(Yank * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfYank"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfYank"/> is scaled by this value.</param>
    public UnitOfYank ScaledBy(Scalar scale) => new(Yank * scale);
    /// <summary>Derives a new <see cref="UnitOfYank"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfYank"/> is scaled by this value.</param>
    public UnitOfYank ScaledBy(double scale) => new(Yank * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfYank other) => Yank.CompareTo(other.Yank);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Yank"/>.</summary>
    public override string ToString() => $"{GetType()}: {Yank}";

    /// <summary>Determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Yank"/> represented by this <see cref="UnitOfYank"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfYank"/>.</param>
    public static bool operator <(UnitOfYank x, UnitOfYank y) => x.Yank < y.Yank;
    /// <summary>Determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Yank"/> represented by this <see cref="UnitOfYank"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfYank"/>.</param>
    public static bool operator >(UnitOfYank x, UnitOfYank y) => x.Yank > y.Yank;
    /// <summary>Determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Yank"/> represented by this <see cref="UnitOfYank"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfYank"/>.</param>
    public static bool operator <=(UnitOfYank x, UnitOfYank y) => x.Yank <= y.Yank;
    /// <summary>Determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Yank"/> represented by this <see cref="UnitOfYank"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Yank"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfYank"/>.</param>
    public static bool operator >=(UnitOfYank x, UnitOfYank y) => x.Yank >= y.Yank;
}
