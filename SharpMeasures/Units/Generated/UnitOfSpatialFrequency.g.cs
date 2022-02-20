#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.SpatialFrequency"/>.</summary>
/// <remarks>Common <see cref="UnitOfSpatialFrequency"/> exists as static properties, and from these custom <see cref="UnitOfSpatialFrequency"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfSpatialFrequency"/> can also be derived from
/// other units using the static <see cref="From(UnitOfLength)"/>.</remarks>
public readonly record struct UnitOfSpatialFrequency :
    IComparable<UnitOfSpatialFrequency>
{
    /// <summary>Derives a <see cref="UnitOfSpatialFrequency"/> according to { 1 / <paramref name="unitOfLength"/> }.</summary>
    /// <param name="unitOfLength">A <see cref="UnitOfSpatialFrequency"/> is derived from the inverse of this <see cref="UnitOfLength"/>.</param>
    public static UnitOfSpatialFrequency From(UnitOfLength unitOfLength) => new(SpatialFrequency.From(unitOfLength.Length));

    /// <summary>The SI unit of <see cref="Quantities.SpatialFrequency"/>, derived according to { 1 / <see cref="UnitOfLength.Metre"/> }.
    /// Usually written as [1/m] or [m⁻¹].</summary>
    public static UnitOfSpatialFrequency PerMetre { get; } = From(UnitOfLength.Metre);

    /// <summary>The <see cref="Quantities.SpatialFrequency"/> that the <see cref="UnitOfSpatialFrequency"/> represents.</summary>
    public SpatialFrequency SpatialFrequency { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfSpatialFrequency"/>, representing the <see cref="Quantities.SpatialFrequency"/> <paramref name="spatialFrequency"/>.</summary>
    /// <param name="spatialFrequency">The <see cref="Quantities.SpatialFrequency"/> that the new <see cref="UnitOfSpatialFrequency"/> represents.</param>
    private UnitOfSpatialFrequency(SpatialFrequency spatialFrequency)
    {
        SpatialFrequency = spatialFrequency;
    }

    /// <summary>Derives a new <see cref="UnitOfSpatialFrequency"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfSpatialFrequency"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfSpatialFrequency WithPrefix(MetricPrefix prefix) => new(SpatialFrequency * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfSpatialFrequency"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSpatialFrequency"/> is scaled by this value.</param>
    public UnitOfSpatialFrequency ScaledBy(Scalar scale) => new(SpatialFrequency * scale);
    /// <summary>Derives a new <see cref="UnitOfSpatialFrequency"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSpatialFrequency"/> is scaled by this value.</param>
    public UnitOfSpatialFrequency ScaledBy(double scale) => new(SpatialFrequency * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfSpatialFrequency other) => SpatialFrequency.CompareTo(other.SpatialFrequency);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.SpatialFrequency"/>.</summary>
    public override string ToString() => $"{GetType()}: {SpatialFrequency}";

    /// <summary>Determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by this <see cref="UnitOfSpatialFrequency"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfSpatialFrequency"/>.</param>
    public static bool operator <(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.SpatialFrequency < y.SpatialFrequency;
    /// <summary>Determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by this <see cref="UnitOfSpatialFrequency"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfSpatialFrequency"/>.</param>
    public static bool operator >(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.SpatialFrequency > y.SpatialFrequency;
    /// <summary>Determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by this <see cref="UnitOfSpatialFrequency"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfSpatialFrequency"/>.</param>
    public static bool operator <=(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.SpatialFrequency <= y.SpatialFrequency;
    /// <summary>Determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by this <see cref="UnitOfSpatialFrequency"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpatialFrequency"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfSpatialFrequency"/>.</param>
    public static bool operator >=(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.SpatialFrequency >= y.SpatialFrequency;
}
