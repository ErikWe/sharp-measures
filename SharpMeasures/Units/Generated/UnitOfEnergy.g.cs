namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantities <see cref="Quantities.Energy"/> and <see cref="Quantities.Work"/>.</summary>
/// <remarks>Common <see cref="UnitOfEnergy"/> exists as static properties, and from these custom <see cref="UnitOfEnergy"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfEnergy"/> can also be derived from
/// other units using the static <see cref="From(UnitOfForce, UnitOfLength)"/>, or an overload.</remarks>
public readonly record struct UnitOfEnergy :
    IComparable<UnitOfEnergy>
{
    /// <summary>Derives a <see cref="UnitOfEnergy"/> according to { <paramref name="unitOfForce"/> ∙ <paramref name="unitOfLength"/> }.</summary>
    /// <param name="unitOfForce">A <see cref="UnitOfEnergy"/> is derived from multiplication of this <see cref="UnitOfForce"/> by <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">A <see cref="UnitOfEnergy"/> is derived from multiplication of this <see cref="UnitOfLength"/> by <paramref name="unitOfForce"/>.</param>
    public static UnitOfEnergy From(UnitOfForce unitOfForce, UnitOfLength unitOfLength) => new(Work.From(unitOfForce.Force, unitOfLength.Length.AsDistance).AsEnergy);
    /// <summary>Derives a <see cref="UnitOfEnergy"/> according to { <paramref name="unitOfPower"/> ∙ <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfPower">A <see cref="UnitOfEnergy"/> is derived from multiplication of this <see cref="UnitOfPower"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfEnergy"/> is derived from multiplication of this <see cref="UnitOfTime"/> by <paramref name="unitOfPower"/>.</param>
    public static UnitOfEnergy From(UnitOfPower unitOfPower, UnitOfTime unitOfTime) => new(Work.From(unitOfPower.Power, unitOfTime.Time).AsEnergy);

    /// <summary>The SI unit of <see cref="Quantities.Energy"/>, derived according to { <see cref="UnitOfForce.Newton"/> ∙ <see cref="UnitOfLength.Metre"/> }.
    /// Usually written as [J].</summary>
    public static UnitOfEnergy Joule { get; } = From(UnitOfForce.Newton, UnitOfLength.Metre);
    /// <summary>Expresses <see cref="Quantities.Energy"/> as one thousand <see cref="Joule"/>. Usually written as [kJ].</summary>
    public static UnitOfEnergy Kilojoule { get; } = Joule.WithPrefix(MetricPrefix.Kilo);
    /// <summary>Expresses <see cref="Quantities.Energy"/> as one million <see cref="Joule"/>. Usually written as [MJ].</summary>
    public static UnitOfEnergy Megajoule { get; } = Joule.WithPrefix(MetricPrefix.Mega);
    /// <summary>Expresses <see cref="Quantities.Energy"/> as one billion [10^9] <see cref="Joule"/>. Usually written as [GJ].</summary>
    public static UnitOfEnergy Gigajoule { get; } = Joule.WithPrefix(MetricPrefix.Giga);
    /// <summary>Expresses <see cref="Quantities.Energy"/> according to { <see cref="UnitOfPower.Kilowatt"/> ∙ <see cref="UnitOfTime.Hour"/> }.
    /// Usually written as [kWh] or [kW∙h].</summary>
    public static UnitOfEnergy KilowattHour { get; } = From(UnitOfPower.Kilowatt, UnitOfTime.Hour);

    /// <summary>Expresses <see cref="Quantities.Energy"/> according to { 4.184 ∙ <see cref="Joule"/> }. Usually written as [cal].</summary>
    /// <remarks>Not to be confused with <see cref="Kilocalorie"/>, which is sometimes also known as calorie.</remarks>
    public static UnitOfEnergy Calorie { get; } = Joule.ScaledBy(4.184);
    /// <summary>Expresses <see cref="Quantities.Energy"/> as one thousand <see cref="Calorie"/>. Usually written as [kcal] or [cal].</summary>
    /// <remarks>This is sometimes also known as calorie, but is not equivalent to <see cref="Calorie"/>.</remarks>
    public static UnitOfEnergy Kilocalorie { get; } = Calorie.WithPrefix(MetricPrefix.Kilo);

    /// <summary>The <see cref="Quantities.Energy"/> that the <see cref="UnitOfEnergy"/> represents.</summary>
    public Energy Energy { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfEnergy"/>, representing the <see cref="Quantities.Energy"/> <paramref name="energy"/>.</summary>
    /// <param name="energy">The <see cref="Quantities.Energy"/> that the new <see cref="UnitOfEnergy"/> represents.</param>
    private UnitOfEnergy(Energy energy)
    {
        Energy = energy;
    }

    /// <summary>Derives a new <see cref="UnitOfEnergy"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfEnergy"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfEnergy WithPrefix(MetricPrefix prefix) => new(Energy * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfEnergy"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfEnergy"/> is scaled by this value.</param>
    public UnitOfEnergy ScaledBy(Scalar scale) => new(Energy * scale);
    /// <summary>Derives a new <see cref="UnitOfEnergy"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfEnergy"/> is scaled by this value.</param>
    public UnitOfEnergy ScaledBy(double scale) => new(Energy * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfEnergy other) => Energy.CompareTo(other.Energy);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Energy"/>.</summary>
    public override string ToString() => $"{GetType()}: {Energy}";

    /// <summary>Determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Energy"/> represented by this <see cref="UnitOfEnergy"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfEnergy"/>.</param>
    public static bool operator <(UnitOfEnergy x, UnitOfEnergy y) => x.Energy < y.Energy;
    /// <summary>Determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Energy"/> represented by this <see cref="UnitOfEnergy"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfEnergy"/>.</param>
    public static bool operator >(UnitOfEnergy x, UnitOfEnergy y) => x.Energy > y.Energy;
    /// <summary>Determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Energy"/> represented by this <see cref="UnitOfEnergy"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfEnergy"/>.</param>
    public static bool operator <=(UnitOfEnergy x, UnitOfEnergy y) => x.Energy <= y.Energy;
    /// <summary>Determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Energy"/> represented by this <see cref="UnitOfEnergy"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Energy"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfEnergy"/>.</param>
    public static bool operator >=(UnitOfEnergy x, UnitOfEnergy y) => x.Energy >= y.Energy;
}
