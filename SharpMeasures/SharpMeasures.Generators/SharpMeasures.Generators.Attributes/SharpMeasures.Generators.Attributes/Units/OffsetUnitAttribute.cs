namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit according to an offset from another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class OffsetUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.
    /// <para>This value is ignored if the unit is a constant.</para></remarks>
    public string Plural { get; }
    /// <summary>The name of the original instance, from which this instance is offset.</summary>
    public string From { get; }
    /// <summary>The size of the offset.</summary>
    public double Offset { get; }

    /// <summary>Constructs a definition of an instance of the unit according to an offset from another instance of the same unit.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para>
    /// <para>This value is ignored if the unit is a constant.</para></param>
    /// <param name="from">The name of the original instance, from which this instance is offset.</param>
    /// <param name="offset">The size of the offset.</param>
    public OffsetUnitAttribute(string name, string plural, string from, double offset)
    {
        Name = name;
        Plural = plural;
        From = from;
        Offset = offset;
    }
}
