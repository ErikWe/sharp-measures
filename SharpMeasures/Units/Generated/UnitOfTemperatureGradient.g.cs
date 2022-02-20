#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.TemperatureGradient"/>.</summary>
/// <remarks>Common <see cref="UnitOfTemperatureGradient"/> exists as static properties, and from these custom <see cref="UnitOfTemperatureGradient"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfTemperatureGradient"/> can also be derived from
/// other units using the static <see cref="From(UnitOfTemperature, UnitOfLength)"/>.</remarks>
public readonly record struct UnitOfTemperatureGradient :
    IComparable<UnitOfTemperatureGradient>
{
    /// <summary>Derives a <see cref="UnitOfTemperatureGradient"/> according to { <paramref name="unitOfTemperature"/> / <paramref name="unitOfLength"/> }.</summary>
    /// <param name="unitOfTemperature">A <see cref="UnitOfTemperatureGradient"/> is derived from division of this <see cref="UnitOfTemperature"/> by <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">A <see cref="UnitOfTemperatureGradient"/> is derived from division of <paramref name="unitOfTemperature"/> by this <see cref="UnitOfLength"/>.</param>
    public static UnitOfTemperatureGradient From(UnitOfTemperature unitOfTemperature, UnitOfLength unitOfLength) 
    	=> new(TemperatureGradient.From(unitOfTemperature.TemperatureDifference, unitOfLength.Length.AsDistance));

    /// <summary>The SI unit of <see cref="Quantities.TemperatureGradient"/>, derived according to { <see cref="UnitOfTemperature.Kelvin"/>
    /// / <see cref="UnitOfLength.Metre"/> }. Usually written as [K/m] or [K∙m⁻¹]</summary>
    /// <remarks>This is equivalent to <see cref="CelsiusPerMetre"/>.</remarks>
    public static UnitOfTemperatureGradient KelvinPerMetre { get; } = From(UnitOfTemperature.Kelvin, UnitOfLength.Metre);
    /// <summary>Expresses <see cref="Quantities.TemperatureGradient"/> according to { <see cref="UnitOfTemperature.Celsius"/> / <see cref="UnitOfLength.Metre"/> }.
    /// Usually written as [°C/m] or [°C∙m⁻¹]</summary>
    /// <remarks>This is equivalent to <see cref="KelvinPerMetre"/>.</remarks>
    public static UnitOfTemperatureGradient CelsiusPerMetre { get; } = KelvinPerMetre;
    /// <summary>Expresses <see cref="Quantities.TemperatureGradient"/> according to { <see cref="UnitOfTemperature.Rankine"/> / <see cref="UnitOfLength.Metre"/> }.
    /// Usually written as [°R/m] or [°R∙m⁻¹].</summary>
    public static UnitOfTemperatureGradient RankinePerMetre { get; } = From(UnitOfTemperature.Rankine, UnitOfLength.Metre);
    /// <summary>Expresses <see cref="Quantities.TemperatureGradient"/> according to { <see cref="UnitOfTemperature.Fahrenheit"/> / <see cref="UnitOfLength.Metre"/> }.
    /// Usually written as [°F/m] or [°F∙m⁻¹].</summary>
    public static UnitOfTemperatureGradient FahrenheitPerMetre { get; } = From(UnitOfTemperature.Fahrenheit, UnitOfLength.Metre);
    /// <summary>Expresses <see cref="Quantities.TemperatureGradient"/> according to { <see cref="UnitOfTemperature.Fahrenheit"/> / <see cref="UnitOfLength.Foot"/> }.
    /// Usually written as [°F/ft] or [°F∙ft⁻¹].</summary>
    public static UnitOfTemperatureGradient FahrenheitPerFoot { get; } = From(UnitOfTemperature.Fahrenheit, UnitOfLength.Foot);

    /// <summary>The <see cref="Quantities.TemperatureGradient"/> that the <see cref="UnitOfTemperatureGradient"/> represents.</summary>
    public TemperatureGradient TemperatureGradient { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfTemperatureGradient"/>, representing the <see cref="Quantities.TemperatureGradient"/> <paramref name="temperatureGradient"/>.</summary>
    /// <param name="temperatureGradient">The <see cref="Quantities.TemperatureGradient"/> that the new <see cref="UnitOfTemperatureGradient"/> represents.</param>
    private UnitOfTemperatureGradient(TemperatureGradient temperatureGradient)
    {
        TemperatureGradient = temperatureGradient;
    }

    /// <summary>Derives a new <see cref="UnitOfTemperatureGradient"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfTemperatureGradient"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfTemperatureGradient WithPrefix(MetricPrefix prefix) => new(TemperatureGradient * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfTemperatureGradient"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTemperatureGradient"/> is scaled by this value.</param>
    public UnitOfTemperatureGradient ScaledBy(Scalar scale) => new(TemperatureGradient * scale);
    /// <summary>Derives a new <see cref="UnitOfTemperatureGradient"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTemperatureGradient"/> is scaled by this value.</param>
    public UnitOfTemperatureGradient ScaledBy(double scale) => new(TemperatureGradient * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfTemperatureGradient other) => TemperatureGradient.CompareTo(other.TemperatureGradient);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.TemperatureGradient"/>.</summary>
    public override string ToString() => $"{GetType()}: {TemperatureGradient}";

    /// <summary>Determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by this <see cref="UnitOfTemperatureGradient"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfTemperatureGradient"/>.</param>
    public static bool operator <(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.TemperatureGradient < y.TemperatureGradient;
    /// <summary>Determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by this <see cref="UnitOfTemperatureGradient"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfTemperatureGradient"/>.</param>
    public static bool operator >(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.TemperatureGradient > y.TemperatureGradient;
    /// <summary>Determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by this <see cref="UnitOfTemperatureGradient"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfTemperatureGradient"/>.</param>
    public static bool operator <=(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.TemperatureGradient <= y.TemperatureGradient;
    /// <summary>Determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by this <see cref="UnitOfTemperatureGradient"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TemperatureGradient"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfTemperatureGradient"/>.</param>
    public static bool operator >=(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.TemperatureGradient >= y.TemperatureGradient;
}
