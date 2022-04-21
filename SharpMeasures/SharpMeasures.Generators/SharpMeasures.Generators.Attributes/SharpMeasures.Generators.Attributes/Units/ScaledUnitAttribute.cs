﻿namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit by scaling of another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScaledUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.
    /// <para>This value is ignored if the unit is a constant.</para></remarks>
    public string Plural { get; }
    /// <summary>The name of the instance that is scaled.</summary>
    public string From { get; }
    /// <summary>The value by which the original instance is scaled.</summary>
    public double Scale { get; }

    /// <summary>Constructs a definition of an instance of the unit by scaling of another instance of the same unit.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para>
    /// <para>This value is ignored if the unit is a constant.</para></param>
    /// <param name="from">The name of the instance that is scaled.</param>
    /// <param name="scale">The value by which the original instance is scaled.</param>
    public ScaledUnitAttribute(string name, string plural, string from, double scale)
    {
        Name = name;
        Plural = plural;
        From = from;
        Scale = scale;
    }
}
