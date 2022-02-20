#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.FrequencyDrift"/>.</summary>
/// <remarks>Common <see cref="UnitOfFrequencyDrift"/> exists as static properties, and from these custom <see cref="UnitOfFrequencyDrift"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfFrequencyDrift"/> can also be derived from
/// other units using the static <see cref="From(UnitOfFrequency, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfFrequencyDrift :
    IComparable<UnitOfFrequencyDrift>
{
    /// <summary>Derives a <see cref="UnitOfFrequencyDrift"/> according to { <paramref name="unitOfFrequency"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfFrequency">A <see cref="UnitOfFrequencyDrift"/> is derived from division of this <see cref="UnitOfFrequency"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfFrequencyDrift"/> is derived from division of <paramref name="unitOfFrequency"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfFrequencyDrift From(UnitOfFrequency unitOfFrequency, UnitOfTime unitOfTime) => new(FrequencyDrift.From(unitOfFrequency.Frequency, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.FrequencyDrift"/>, derived according to { <see cref="UnitOfFrequency.Hertz"/> /
    /// <see cref="UnitOfTime.Second"/> }. Usually written as [Hz/s] or [Hz∙s⁻¹].</summary>
    public static UnitOfFrequencyDrift HertzPerSecond { get; } = From(UnitOfFrequency.Hertz, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.FrequencyDrift"/> according to { <see cref="UnitOfFrequency.PerSecond"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [1/s²] or [s⁻²].</summary>
    public static UnitOfFrequencyDrift PerSecondSquared { get; } = From(UnitOfFrequency.PerSecond, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.FrequencyDrift"/> that the <see cref="UnitOfFrequencyDrift"/> represents.</summary>
    public FrequencyDrift FrequencyDrift { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfFrequencyDrift"/>, representing the <see cref="Quantities.FrequencyDrift"/> <paramref name="frequencyDrift"/>.</summary>
    /// <param name="frequencyDrift">The <see cref="Quantities.FrequencyDrift"/> that the new <see cref="UnitOfFrequencyDrift"/> represents.</param>
    private UnitOfFrequencyDrift(FrequencyDrift frequencyDrift)
    {
        FrequencyDrift = frequencyDrift;
    }

    /// <summary>Derives a new <see cref="UnitOfFrequencyDrift"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfFrequencyDrift"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfFrequencyDrift WithPrefix(MetricPrefix prefix) => new(FrequencyDrift * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfFrequencyDrift"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfFrequencyDrift"/> is scaled by this value.</param>
    public UnitOfFrequencyDrift ScaledBy(Scalar scale) => new(FrequencyDrift * scale);
    /// <summary>Derives a new <see cref="UnitOfFrequencyDrift"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfFrequencyDrift"/> is scaled by this value.</param>
    public UnitOfFrequencyDrift ScaledBy(double scale) => new(FrequencyDrift * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfFrequencyDrift other) => FrequencyDrift.CompareTo(other.FrequencyDrift);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.FrequencyDrift"/>.</summary>
    public override string ToString() => $"{GetType()}: {FrequencyDrift}";

    /// <summary>Determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by this <see cref="UnitOfFrequencyDrift"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfFrequencyDrift"/>.</param>
    public static bool operator <(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.FrequencyDrift < y.FrequencyDrift;
    /// <summary>Determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by this <see cref="UnitOfFrequencyDrift"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfFrequencyDrift"/>.</param>
    public static bool operator >(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.FrequencyDrift > y.FrequencyDrift;
    /// <summary>Determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by this <see cref="UnitOfFrequencyDrift"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfFrequencyDrift"/>.</param>
    public static bool operator <=(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.FrequencyDrift <= y.FrequencyDrift;
    /// <summary>Determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by this <see cref="UnitOfFrequencyDrift"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.FrequencyDrift"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfFrequencyDrift"/>.</param>
    public static bool operator >=(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.FrequencyDrift >= y.FrequencyDrift;
}
