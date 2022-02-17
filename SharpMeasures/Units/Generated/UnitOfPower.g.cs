namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Power"/>.</summary>
/// <remarks>Common <see cref="UnitOfPower"/> exists as static properties, and from these custom <see cref="UnitOfPower"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfPower"/> can also be derived from
/// other units using the static <see cref="From(UnitOfEnergy, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfPower :
    IComparable<UnitOfPower>
{
    /// <summary>Derives a <see cref="UnitOfPower"/> according to { <paramref name="unitOfEnergy"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfEnergy">A <see cref="UnitOfPower"/> is derived from division of this <see cref="UnitOfEnergy"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfPower"/> is derived from division of <paramref name="unitOfEnergy"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfPower From(UnitOfEnergy unitOfEnergy, UnitOfTime unitOfTime) => new(Power.From(unitOfEnergy.Energy.AsWork, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Power"/>, derived according to { <see cref="UnitOfEnergy.Joule"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [W].</summary>
    public static UnitOfPower Watt { get; } = From(UnitOfEnergy.Joule, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Power"/> as one thousand <see cref="Watt"/>. Usually written as [kW].</summary>
    public static UnitOfPower Kilowatt { get; } = Watt.WithPrefix(MetricPrefix.Kilo);
    /// <summary>Expresses <see cref="Quantities.Power"/> as one million <see cref="Watt"/>. Usually written as [MW].</summary>
    public static UnitOfPower Megawatt { get; } = Watt.WithPrefix(MetricPrefix.Mega);
    /// <summary>Expresses <see cref="Quantities.Power"/> as one billion [10^9] <see cref="Watt"/>. Usually written as [GW].</summary>
    public static UnitOfPower Gigawatt { get; } = Watt.WithPrefix(MetricPrefix.Giga);
    /// <summary>Expresses <see cref="Quantities.Power"/> as one trillion [10^12] <see cref="Watt"/>. Usually written as [TW].</summary>
    public static UnitOfPower Terawatt { get; } = Watt.WithPrefix(MetricPrefix.Tera);

    /// <summary>The <see cref="Quantities.Power"/> that the <see cref="UnitOfPower"/> represents.</summary>
    public Power Power { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfPower"/>, representing the <see cref="Quantities.Power"/> <paramref name="power"/>.</summary>
    /// <param name="power">The <see cref="Quantities.Power"/> that the new <see cref="UnitOfPower"/> represents.</param>
    private UnitOfPower(Power power)
    {
        Power = power;
    }

    /// <summary>Derives a new <see cref="UnitOfPower"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfPower"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfPower WithPrefix(MetricPrefix prefix) => new(Power * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfPower"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfPower"/> is scaled by this value.</param>
    public UnitOfPower ScaledBy(Scalar scale) => new(Power * scale);
    /// <summary>Derives a new <see cref="UnitOfPower"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfPower"/> is scaled by this value.</param>
    public UnitOfPower ScaledBy(double scale) => new(Power * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfPower other) => Power.CompareTo(other.Power);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Power"/>.</summary>
    public override string ToString() => $"{GetType()}: {Power}";

    /// <summary>Determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Power"/> represented by this <see cref="UnitOfPower"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfPower"/>.</param>
    public static bool operator <(UnitOfPower x, UnitOfPower y) => x.Power < y.Power;
    /// <summary>Determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Power"/> represented by this <see cref="UnitOfPower"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfPower"/>.</param>
    public static bool operator >(UnitOfPower x, UnitOfPower y) => x.Power > y.Power;
    /// <summary>Determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Power"/> represented by this <see cref="UnitOfPower"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfPower"/>.</param>
    public static bool operator <=(UnitOfPower x, UnitOfPower y) => x.Power <= y.Power;
    /// <summary>Determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Power"/> represented by this <see cref="UnitOfPower"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Power"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfPower"/>.</param>
    public static bool operator >=(UnitOfPower x, UnitOfPower y) => x.Power >= y.Power;
}
