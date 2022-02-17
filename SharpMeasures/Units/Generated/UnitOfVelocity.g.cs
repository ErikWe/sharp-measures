namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Speed"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfVelocity"/> exists as static properties, and from these custom <see cref="UnitOfVelocity"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfVelocity"/> can also be derived from
/// other units using the static <see cref="From(UnitOfLength, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfVelocity :
    IComparable<UnitOfVelocity>
{
    /// <summary>Derives a <see cref="UnitOfVelocity"/> according to { <paramref name="unitOfLength"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfLength">A <see cref="UnitOfVelocity"/> is derived from division of this <see cref="UnitOfLength"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfVelocity"/> is derived from division of <paramref name="unitOfLength"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfVelocity From(UnitOfLength unitOfLength, UnitOfTime unitOfTime) => new(Speed.From(unitOfLength.Length.AsDistance, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Speed"/>, derived according to { <see cref="UnitOfLength.Metre"/> /
    /// <see cref="UnitOfTime.Second"/> }. Usually written as [m/s], [m∙s⁻¹], or [mps].</summary>
    public static UnitOfVelocity MetrePerSecond { get; } = From(UnitOfLength.Metre, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Speed"/> according to { <see cref="UnitOfLength.Kilometre"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [km/s] or [km∙s⁻¹].</summary>
    public static UnitOfVelocity KilometrePerSecond { get; } = From(UnitOfLength.Kilometre, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Speed"/> according to { <see cref="UnitOfLength.Kilometre"/> / <see cref="UnitOfTime.Hour"/> }. Usually written as [km/h],
    /// [km/hr], [kph], or [kmph].</summary>
    public static UnitOfVelocity KilometrePerHour { get; } = From(UnitOfLength.Kilometre, UnitOfTime.Hour);
    /// <summary>Expresses <see cref="Quantities.Speed"/> according to { <see cref="UnitOfLength.Foot"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [ft/s] or [ft∙s⁻¹].</summary>
    public static UnitOfVelocity FootPerSecond { get; } = From(UnitOfLength.Foot, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Speed"/> according to { <see cref="UnitOfLength.Mile"/> / <see cref="UnitOfTime.Hour"/> }.
    /// Usually written as [mi/h] or [mph].</summary>
    public static UnitOfVelocity MilePerHour { get; } = From(UnitOfLength.Mile, UnitOfTime.Hour);

    /// <summary>The <see cref="Quantities.Speed"/> that the <see cref="UnitOfVelocity"/> represents.</summary>
    public Speed Speed { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfVelocity"/>, representing the <see cref="Quantities.Speed"/> <paramref name="speed"/>.</summary>
    /// <param name="speed">The <see cref="Quantities.Speed"/> that the new <see cref="UnitOfVelocity"/> represents.</param>
    private UnitOfVelocity(Speed speed)
    {
        Speed = speed;
    }

    /// <summary>Derives a new <see cref="UnitOfVelocity"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfVelocity"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfVelocity WithPrefix(MetricPrefix prefix) => new(Speed * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfVelocity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVelocity"/> is scaled by this value.</param>
    public UnitOfVelocity ScaledBy(Scalar scale) => new(Speed * scale);
    /// <summary>Derives a new <see cref="UnitOfVelocity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVelocity"/> is scaled by this value.</param>
    public UnitOfVelocity ScaledBy(double scale) => new(Speed * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfVelocity other) => Speed.CompareTo(other.Speed);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Speed"/>.</summary>
    public override string ToString() => $"{GetType()}: {Speed}";

    /// <summary>Determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Speed"/> represented by this <see cref="UnitOfVelocity"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfVelocity"/>.</param>
    public static bool operator <(UnitOfVelocity x, UnitOfVelocity y) => x.Speed < y.Speed;
    /// <summary>Determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Speed"/> represented by this <see cref="UnitOfVelocity"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfVelocity"/>.</param>
    public static bool operator >(UnitOfVelocity x, UnitOfVelocity y) => x.Speed > y.Speed;
    /// <summary>Determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Speed"/> represented by this <see cref="UnitOfVelocity"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfVelocity"/>.</param>
    public static bool operator <=(UnitOfVelocity x, UnitOfVelocity y) => x.Speed <= y.Speed;
    /// <summary>Determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Speed"/> represented by this <see cref="UnitOfVelocity"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Speed"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfVelocity"/>.</param>
    public static bool operator >=(UnitOfVelocity x, UnitOfVelocity y) => x.Speed >= y.Speed;
}
