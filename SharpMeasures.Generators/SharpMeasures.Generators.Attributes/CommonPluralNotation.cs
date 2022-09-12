namespace SharpMeasures.Generators;

/// <summary>Defines some common notations for producing the plural form of a name, based on the singular form.</summary>
public static class CommonPluralNotation
{
    /// <summary>Uses the unmodified singular form.</summary>
    public const string Unmodified = "[*]";

    /// <summary>Appends the lower-cased "s" to the singular form.</summary>
    /// <remarks>For example, a singular form "Metre" would have the plural form "Metres".</remarks>
    public const string AppendS = "[*]s";

    /// <summary>Appends the lower-cased "es" to the singular form.</summary>
    /// <remarks>For example, a singular form "Inch" would have the plural form "Inches".</remarks>
    public const string AppendEs = "[*]es";

    /// <summary>Inserts the lower-cased "s" before the first occcurence of "Per" in the singular form.</summary>
    /// <remarks>For example, a singular form "MetrePerSecond" would have the plural form "MetresPerSecond".</remarks>
    public const string InsertSBeforePer = "s[Per]";

    /// <summary>Inserts the lower-cased "es" before the first occcurence of "Per" in the singular form.</summary>
    /// <remarks>For example, a singular form "InchPerSecond" would have the plural form "InchesPerSecond".</remarks>
    public const string InsertEsBeforePer = "es[Per]";

    /// <summary>Appends "Multiples" to the singular form.</summary>
    /// <remarks>For example, a singular form "StandardGravity" would result in "StandardGravityMultiples".</remarks>
    public const string AppendMultiples = "[*]Multiples";

    /// <summary>Prepends "MultiplesOf" to the singular form.</summary>
    /// <remarks>For example, a singular form "StandardGravity" would result in "MultiplesOfStandardGravity".</remarks>
    public const string PrependMultiplesOf = "MultiplesOf[*]";

    /// <summary>Prepends "In" to the singular form.</summary>
    /// <remarks>For example, a singular form "StandardGravity" would result in "InStandardGravity".</remarks>
    public const string PrependIn = "In[*]";

    /// <summary>Prepends "In" and appends "Multiples" to the singular form.</summary>
    /// <remarks>For example, a singular form "StandardGravity" would result in "InStandardGravityMultiples".</remarks>
    public const string PrependInAndAppendMultiples = "In[*]Multiples";

    /// <summary>Prepends "InMultiplesOf" to the singular form.</summary>
    /// <remarks>For example, a singular form "StandardGravity" would result in "InMultiplesOfStandardGravity".</remarks>
    public const string PrependInMultiplesOf = "InMultiplesOf[*]";

    /// <summary>Prepends "In" and appends the lower-cased "s" to the singular form.</summary>
    /// <remarks>For example, a singular form "PlanckLength" would result in "InPlanckLengths".</remarks>
    public const string PrependInAndAppendS = "In[*]s";

    /// <summary>Prepends "In" and appends the lower-cased "es" to the name of the constant.</summary>
    /// <remarks>For example, a singular form "PlanckMass" would result in "InPlanckMasses".</remarks>
    public const string PrependInAndAppendEs = "In[*]es";
}
