namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Impulse"/>, and related quantities</summary>
/// <remarks>Common <see cref="UnitOfImpulse"/> exists as static properties, and from these custom <see cref="UnitOfImpulse"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfImpulse"/> can also be derived from
/// other units using the static <see cref="From(UnitOfForce, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfImpulse :
    IComparable<UnitOfImpulse>
{
    /// <summary>Derives a <see cref="UnitOfImpulse"/> according to { <paramref name="unitOfForce"/> ∙ <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfForce">A <see cref="UnitOfImpulse"/> is derived from multiplication of this <see cref="UnitOfForce"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfImpulse"/> is derived from multiplication of this <see cref="UnitOfTime"/> by <paramref name="unitOfForce"/>.</param>
    public static UnitOfImpulse From(UnitOfForce unitOfForce, UnitOfTime unitOfTime) => new(Impulse.From(unitOfForce.Force, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Impulse"/>, derived according to { <see cref="UnitOfForce.Newton"/> ∙ <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [N∙s].</summary>
    public static UnitOfImpulse NewtonSecond { get; } = From(UnitOfForce.Newton, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.Impulse"/> that the <see cref="UnitOfImpulse"/> represents.</summary>
    public Impulse Impulse { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfImpulse"/>, representing the <see cref="Quantities.Impulse"/> <paramref name="impulse"/>.</summary>
    /// <param name="impulse">The <see cref="Quantities.Impulse"/> that the new <see cref="UnitOfImpulse"/> represents.</param>
    private UnitOfImpulse(Impulse impulse)
    {
        Impulse = impulse;
    }

    /// <summary>Derives a new <see cref="UnitOfImpulse"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfImpulse"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfImpulse WithPrefix(MetricPrefix prefix) => new(Impulse * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfImpulse"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfImpulse"/> is scaled by this value.</param>
    public UnitOfImpulse ScaledBy(Scalar scale) => new(Impulse * scale);
    /// <summary>Derives a new <see cref="UnitOfImpulse"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfImpulse"/> is scaled by this value.</param>
    public UnitOfImpulse ScaledBy(double scale) => new(Impulse * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfImpulse other) => Impulse.CompareTo(other.Impulse);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Impulse"/>.</summary>
    public override string ToString() => $"{GetType()}: {Impulse}";

    /// <summary>Determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Impulse"/> represented by this <see cref="UnitOfImpulse"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfImpulse"/>.</param>
    public static bool operator <(UnitOfImpulse x, UnitOfImpulse y) => x.Impulse < y.Impulse;
    /// <summary>Determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Impulse"/> represented by this <see cref="UnitOfImpulse"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfImpulse"/>.</param>
    public static bool operator >(UnitOfImpulse x, UnitOfImpulse y) => x.Impulse > y.Impulse;
    /// <summary>Determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Impulse"/> represented by this <see cref="UnitOfImpulse"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfImpulse"/>.</param>
    public static bool operator <=(UnitOfImpulse x, UnitOfImpulse y) => x.Impulse <= y.Impulse;
    /// <summary>Determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Impulse"/> represented by this <see cref="UnitOfImpulse"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Impulse"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfImpulse"/>.</param>
    public static bool operator >=(UnitOfImpulse x, UnitOfImpulse y) => x.Impulse >= y.Impulse;
}
