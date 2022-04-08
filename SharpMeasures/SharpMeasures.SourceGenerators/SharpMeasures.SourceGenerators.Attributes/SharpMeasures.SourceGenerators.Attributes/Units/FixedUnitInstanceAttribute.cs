namespace SharpMeasures.Attributes;

using System;

/// <summary>Defines an instance of the unit, using a fixed value.</summary>
/// <remarks>It is recommended to only define the SI unit using <see cref="FixedUnitInstanceAttribute"/>, and then
/// derive other instances based on that instance - using, for example, <see cref="ScaledUnitInstanceAttribute"/>.
/// <para>This attribute is expected to be used in conjunction with <see cref="UnitAttribute"/> or <see cref="BiasedUnitAttribute"/>.</para></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class FixedUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</remarks>
    public string Plural { get; }
    /// <summary>The symbol of the instance of the unit.</summary>
    public string Symbol { get; init; } = string.Empty;
    /// <summary>Indicates whether this is a SI unit.</summary>
    public bool IsSIUnit { get; init; }
    /// <summary>Indicates whether this is a constant.</summary>
    public bool IsConstant { get; init; }
    /// <summary>The fixed value of the instance of the unit.</summary>
    public double Value { get; }
    /// <summary>The bias of the instance of the unit.</summary>
    /// <remarks>This is only used if the unit is biased.</remarks>
    public double Bias { get; init; }

    /// <summary>Constructs a definition of an instance of the unit, using a fixed value.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.</param>
    /// <param name="value">The fixed value of the instance of the unit.</param>
    /// <param name="symbol">The symbol of the instance of the unit.</param>
    /// <param name="isSIUnit">Indicates whether this is a SI unit.</param>
    /// <param name="isConstant">Indicates whether this is a constant.</param>
    /// <param name="bias">The bias of the instance of the unit.</param>
    public FixedUnitInstanceAttribute(string name, string plural, double value)
    {
        Name = name;
        Plural = plural;
        Value = value;
    }
}
