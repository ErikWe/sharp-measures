namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.SpecificVolume"/>.</summary>
/// <remarks>Common <see cref="UnitOfSpecificVolume"/> exists as static properties, and from these custom <see cref="UnitOfSpecificVolume"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfSpecificVolume"/> can also be derived from
/// other units using the static <see cref="From(UnitOfVolume, UnitOfMass)"/>.</remarks>
public readonly record struct UnitOfSpecificVolume :
    IComparable<UnitOfSpecificVolume>
{
    /// <summary>Derives a <see cref="UnitOfSpecificVolume"/> according to { <paramref name="unitOfVolume"/> / <paramref name="unitOfMass"/> }.</summary>
    /// <param name="unitOfVolume">A <see cref="UnitOfSpecificVolume"/> is derived from division of this <see cref="UnitOfVolume"/> by <paramref name="unitOfMass"/>.</param>
    /// <param name="unitOfMass">A <see cref="UnitOfSpecificVolume"/> is derived from division of <paramref name="unitOfVolume"/> by this <see cref="UnitOfMass"/>.</param>
    public static UnitOfSpecificVolume From(UnitOfVolume unitOfVolume, UnitOfMass unitOfMass) => new(SpecificVolume.From(unitOfVolume.Volume, unitOfMass.Mass));

    /// <summary>The SI unit of <see cref="Quantities.SpecificVolume"/>, derived according to {
    /// <see cref="UnitOfVolume.CubicMetre"/> / <see cref="UnitOfMass.Kilogram"/> }. Usually written as [m³/kg] or [m³∙kg⁻¹].</summary>
    public static UnitOfSpecificVolume CubicMetrePerKilogram { get; } = From(UnitOfVolume.CubicMetre, UnitOfMass.Kilogram);

    /// <summary>The <see cref="Quantities.SpecificVolume"/> that the <see cref="UnitOfSpecificVolume"/> represents.</summary>
    public SpecificVolume SpecificVolume { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfSpecificVolume"/>, representing the <see cref="Quantities.SpecificVolume"/> <paramref name="specificVolume"/>.</summary>
    /// <param name="specificVolume">The <see cref="Quantities.SpecificVolume"/> that the new <see cref="UnitOfSpecificVolume"/> represents.</param>
    private UnitOfSpecificVolume(SpecificVolume specificVolume)
    {
        SpecificVolume = specificVolume;
    }

    /// <summary>Derives a new <see cref="UnitOfSpecificVolume"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfSpecificVolume"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfSpecificVolume WithPrefix(MetricPrefix prefix) => new(SpecificVolume * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfSpecificVolume"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSpecificVolume"/> is scaled by this value.</param>
    public UnitOfSpecificVolume ScaledBy(Scalar scale) => new(SpecificVolume * scale);
    /// <summary>Derives a new <see cref="UnitOfSpecificVolume"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSpecificVolume"/> is scaled by this value.</param>
    public UnitOfSpecificVolume ScaledBy(double scale) => new(SpecificVolume * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfSpecificVolume other) => SpecificVolume.CompareTo(other.SpecificVolume);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.SpecificVolume"/>.</summary>
    public override string ToString() => $"{GetType()}: {SpecificVolume}";

    /// <summary>Determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by this <see cref="UnitOfSpecificVolume"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfSpecificVolume"/>.</param>
    public static bool operator <(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.SpecificVolume < y.SpecificVolume;
    /// <summary>Determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by this <see cref="UnitOfSpecificVolume"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfSpecificVolume"/>.</param>
    public static bool operator >(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.SpecificVolume > y.SpecificVolume;
    /// <summary>Determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by this <see cref="UnitOfSpecificVolume"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfSpecificVolume"/>.</param>
    public static bool operator <=(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.SpecificVolume <= y.SpecificVolume;
    /// <summary>Determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by this <see cref="UnitOfSpecificVolume"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificVolume"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfSpecificVolume"/>.</param>
    public static bool operator >=(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.SpecificVolume >= y.SpecificVolume;
}
