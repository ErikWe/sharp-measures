namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.AngularSpeed"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfAngularVelocity"/> exists as static properties, and from these custom <see cref="UnitOfAngularVelocity"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfAngularVelocity"/> can also be derived from
/// other units using the static <see cref="From(UnitOfAngle, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfAngularVelocity :
    IComparable<UnitOfAngularVelocity>
{
    /// <summary>Derives a <see cref="UnitOfAngularVelocity"/> according to { <paramref name="unitOfAngle"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfAngle">A <see cref="UnitOfAngularVelocity"/> is derived from division of this <see cref="UnitOfAngle"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfAngularVelocity"/> is derived from division of <paramref name="unitOfAngle"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfAngularVelocity From(UnitOfAngle unitOfAngle, UnitOfTime unitOfTime) => new(AngularSpeed.From(unitOfAngle.Angle, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.AngularSpeed"/>, derived according to {
    /// <see cref="UnitOfAngle.Radian"/> / <see cref="UnitOfTime.Second"/> }. Usually written as [rad/s] or [rad∙s⁻¹].</summary>
    public static UnitOfAngularVelocity RadianPerSecond { get; } = From(UnitOfAngle.Radian, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.AngularSpeed"/> according to { <see cref="UnitOfAngle.Degree"/> / <see cref="UnitOfTime.Second"/> }. Usually written as
    /// [°/s] or [deg/s].</summary>
    public static UnitOfAngularVelocity DegreePerSecond { get; } = From(UnitOfAngle.Degree, UnitOfTime.Second);

    /// <summary>Expresses <see cref="Quantities.AngularSpeed"/> according to { <see cref="UnitOfAngle.Turn"/> / <see cref="UnitOfTime.Second"/> }. Usually written as
    /// [rps], [rev/s], [cyc/s], or [rot/s].[</summary>
    public static UnitOfAngularVelocity RevolutionPerSecond { get; } = From(UnitOfAngle.Turn, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.AngularSpeed"/> according to { <see cref="UnitOfAngle.Turn"/> / <see cref="UnitOfTime.Minute"/> }. Usually written as
    /// [rpm], [r/min], [rev/min], or [rot/min].</summary>
    public static UnitOfAngularVelocity RevolutionPerMinute { get; } = From(UnitOfAngle.Turn, UnitOfTime.Minute);

    /// <summary>The <see cref="Quantities.AngularSpeed"/> that the <see cref="UnitOfAngularVelocity"/> represents.</summary>
    public AngularSpeed AngularSpeed { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfAngularVelocity"/>, representing the <see cref="Quantities.AngularSpeed"/> <paramref name="angularSpeed"/>.</summary>
    /// <param name="angularSpeed">The <see cref="Quantities.AngularSpeed"/> that the new <see cref="UnitOfAngularVelocity"/> represents.</param>
    private UnitOfAngularVelocity(AngularSpeed angularSpeed)
    {
        AngularSpeed = angularSpeed;
    }

    /// <summary>Derives a new <see cref="UnitOfAngularVelocity"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfAngularVelocity"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfAngularVelocity WithPrefix(MetricPrefix prefix) => new(AngularSpeed * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfAngularVelocity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngularVelocity"/> is scaled by this value.</param>
    public UnitOfAngularVelocity ScaledBy(Scalar scale) => new(AngularSpeed * scale);
    /// <summary>Derives a new <see cref="UnitOfAngularVelocity"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngularVelocity"/> is scaled by this value.</param>
    public UnitOfAngularVelocity ScaledBy(double scale) => new(AngularSpeed * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfAngularVelocity other) => AngularSpeed.CompareTo(other.AngularSpeed);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.AngularSpeed"/>.</summary>
    public override string ToString() => $"{GetType()}: {AngularSpeed}";

    /// <summary>Determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by this <see cref="UnitOfAngularVelocity"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfAngularVelocity"/>.</param>
    public static bool operator <(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.AngularSpeed < y.AngularSpeed;
    /// <summary>Determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by this <see cref="UnitOfAngularVelocity"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfAngularVelocity"/>.</param>
    public static bool operator >(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.AngularSpeed > y.AngularSpeed;
    /// <summary>Determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by this <see cref="UnitOfAngularVelocity"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfAngularVelocity"/>.</param>
    public static bool operator <=(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.AngularSpeed <= y.AngularSpeed;
    /// <summary>Determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by this <see cref="UnitOfAngularVelocity"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularSpeed"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfAngularVelocity"/>.</param>
    public static bool operator >=(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.AngularSpeed >= y.AngularSpeed;
}
