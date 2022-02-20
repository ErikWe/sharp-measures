#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Pressure"/>.</summary>
/// <remarks>Common <see cref="UnitOfPressure"/> exists as static properties, and from these custom <see cref="UnitOfPressure"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfPressure"/> can also be derived from
/// other units using the static <see cref="From(UnitOfForce, UnitOfArea)"/>.</remarks>
public readonly record struct UnitOfPressure :
    IComparable<UnitOfPressure>
{
    /// <summary>Derives a <see cref="UnitOfPressure"/> according to { <paramref name="unitOfForce"/> / <paramref name="unitOfArea"/> }.</summary>
    /// <param name="unitOfForce">A <see cref="UnitOfPressure"/> is derived from division of this <see cref="UnitOfEnergy"/> by <paramref name="unitOfArea"/>.</param>
    /// <param name="unitOfArea">A <see cref="UnitOfPressure"/> is derived from division of <paramref name="unitOfForce"/> by this <see cref="UnitOfArea"/>.</param>
    public static UnitOfPressure From(UnitOfForce unitOfForce, UnitOfArea unitOfArea) => new(Pressure.From(unitOfForce.Force, unitOfArea.Area));

    /// <summary>The SI unit of <see cref="Quantities.Pressure"/>, derived according to { <see cref="UnitOfForce.Newton"/> / <see cref="UnitOfArea.SquareMetre"/> }.
    /// Usually written as [Pa].</summary>
    public static UnitOfPressure Pascal { get; } = From(UnitOfForce.Newton, UnitOfArea.SquareMetre);
    /// <summary>Expresses <see cref="Quantities.Pressure"/> as one thousand <see cref="Pascal"/>. Usually written as [kPa].</summary>
    public static UnitOfPressure Kilopascal { get; } = Pascal.WithPrefix(MetricPrefix.Kilo);

    /// <summary>Expresses <see cref="Quantities.Pressure"/> as one hundred <see cref="Kilopascal"/>. Usually written as [bar].</summary>
    public static UnitOfPressure Bar { get; } = Kilopascal.ScaledBy(100);
    /// <summary>Expresses <see cref="Quantities.Pressure"/> in terms of the standard pressure at sea level on Earth, according to { 101 325 ∙ <see cref="Pascal"/> }.
    /// Usually written as [atm].</summary>
    public static UnitOfPressure StandardAtmosphere { get; } = Pascal.ScaledBy(101325);

    /// <summary>Expresses <see cref="Quantities.Pressure"/> according to { <see cref="UnitOfForce.PoundForce"/> / <see cref="UnitOfArea.SquareInch"/> }.
    /// Usually written as [psi].</summary>
    public static UnitOfPressure PoundForcePerSquareInch { get; } = From(UnitOfForce.PoundForce, UnitOfArea.SquareInch);

    /// <summary>The <see cref="Quantities.Pressure"/> that the <see cref="UnitOfPressure"/> represents.</summary>
    public Pressure Pressure { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfPressure"/>, representing the <see cref="Quantities.Pressure"/> <paramref name="pressure"/>.</summary>
    /// <param name="pressure">The <see cref="Quantities.Pressure"/> that the new <see cref="UnitOfPressure"/> represents.</param>
    private UnitOfPressure(Pressure pressure)
    {
        Pressure = pressure;
    }

    /// <summary>Derives a new <see cref="UnitOfPressure"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfPressure"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfPressure WithPrefix(MetricPrefix prefix) => new(Pressure * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfPressure"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfPressure"/> is scaled by this value.</param>
    public UnitOfPressure ScaledBy(Scalar scale) => new(Pressure * scale);
    /// <summary>Derives a new <see cref="UnitOfPressure"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfPressure"/> is scaled by this value.</param>
    public UnitOfPressure ScaledBy(double scale) => new(Pressure * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfPressure other) => Pressure.CompareTo(other.Pressure);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Pressure"/>.</summary>
    public override string ToString() => $"{GetType()}: {Pressure}";

    /// <summary>Determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Pressure"/> represented by this <see cref="UnitOfPressure"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfPressure"/>.</param>
    public static bool operator <(UnitOfPressure x, UnitOfPressure y) => x.Pressure < y.Pressure;
    /// <summary>Determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Pressure"/> represented by this <see cref="UnitOfPressure"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfPressure"/>.</param>
    public static bool operator >(UnitOfPressure x, UnitOfPressure y) => x.Pressure > y.Pressure;
    /// <summary>Determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Pressure"/> represented by this <see cref="UnitOfPressure"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfPressure"/>.</param>
    public static bool operator <=(UnitOfPressure x, UnitOfPressure y) => x.Pressure <= y.Pressure;
    /// <summary>Determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Pressure"/> represented by this <see cref="UnitOfPressure"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Pressure"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfPressure"/>.</param>
    public static bool operator >=(UnitOfPressure x, UnitOfPressure y) => x.Pressure >= y.Pressure;
}
