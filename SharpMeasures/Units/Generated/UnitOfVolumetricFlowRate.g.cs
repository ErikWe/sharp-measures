namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.VolumetricFlowRate"/>.</summary>
/// <remarks>Common <see cref="UnitOfVolumetricFlowRate"/> exists as static properties, and from these custom <see cref="UnitOfVolumetricFlowRate"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfVolumetricFlowRate"/> can also be derived from
/// other units using the static <see cref="From(UnitOfVolume, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfVolumetricFlowRate :
    IComparable<UnitOfVolumetricFlowRate>
{
    /// <summary>Derives a <see cref="UnitOfVolumetricFlowRate"/> according to { <paramref name="unitOfVolume"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfVolume">A <see cref="UnitOfVolumetricFlowRate"/> is derived from division of this <see cref="UnitOfVolume"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfVolumetricFlowRate"/> is derived from division of <paramref name="unitOfVolume"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfVolumetricFlowRate From(UnitOfVolume unitOfVolume, UnitOfTime unitOfTime) => new(VolumetricFlowRate.From(unitOfVolume.Volume, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.VolumetricFlowRate"/>, derived according to { <see cref="UnitOfVolume.CubicMetre"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [m³/s] or [m³∙s⁻¹].</summary>
    public static UnitOfVolumetricFlowRate CubicMetrePerSecond { get; } = From(UnitOfVolume.CubicMetre, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.VolumetricFlowRate"/> according to { <see cref="UnitOfVolume.Litre"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [L/s], [l/s], [L∙s⁻¹], or [l∙s⁻¹].</summary>
    public static UnitOfVolumetricFlowRate LitrePerSecond { get; } = From(UnitOfVolume.Litre, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.VolumetricFlowRate"/> that the <see cref="UnitOfVolumetricFlowRate"/> represents.</summary>
    public VolumetricFlowRate VolumetricFlowRate { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfVolumetricFlowRate"/>, representing the <see cref="Quantities.VolumetricFlowRate"/> <paramref name="volumetricFlowRate"/>.</summary>
    /// <param name="volumetricFlowRate">The <see cref="Quantities.VolumetricFlowRate"/> that the new <see cref="UnitOfVolumetricFlowRate"/> represents.</param>
    private UnitOfVolumetricFlowRate(VolumetricFlowRate volumetricFlowRate)
    {
        VolumetricFlowRate = volumetricFlowRate;
    }

    /// <summary>Derives a new <see cref="UnitOfVolumetricFlowRate"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfVolumetricFlowRate"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfVolumetricFlowRate WithPrefix(MetricPrefix prefix) => new(VolumetricFlowRate * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfVolumetricFlowRate"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVolumetricFlowRate"/> is scaled by this value.</param>
    public UnitOfVolumetricFlowRate ScaledBy(Scalar scale) => new(VolumetricFlowRate * scale);
    /// <summary>Derives a new <see cref="UnitOfVolumetricFlowRate"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVolumetricFlowRate"/> is scaled by this value.</param>
    public UnitOfVolumetricFlowRate ScaledBy(double scale) => new(VolumetricFlowRate * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfVolumetricFlowRate other) => VolumetricFlowRate.CompareTo(other.VolumetricFlowRate);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.VolumetricFlowRate"/>.</summary>
    public override string ToString() => $"{GetType()}: {VolumetricFlowRate}";

    /// <summary>Determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by this <see cref="UnitOfVolumetricFlowRate"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfVolumetricFlowRate"/>.</param>
    public static bool operator <(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.VolumetricFlowRate < y.VolumetricFlowRate;
    /// <summary>Determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by this <see cref="UnitOfVolumetricFlowRate"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfVolumetricFlowRate"/>.</param>
    public static bool operator >(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.VolumetricFlowRate > y.VolumetricFlowRate;
    /// <summary>Determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by this <see cref="UnitOfVolumetricFlowRate"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfVolumetricFlowRate"/>.</param>
    public static bool operator <=(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.VolumetricFlowRate <= y.VolumetricFlowRate;
    /// <summary>Determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by this <see cref="UnitOfVolumetricFlowRate"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.VolumetricFlowRate"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfVolumetricFlowRate"/>.</param>
    public static bool operator >=(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.VolumetricFlowRate >= y.VolumetricFlowRate;
}
