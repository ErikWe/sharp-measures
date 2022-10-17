namespace SharpMeasures.Generators;

/// <summary>Defines some common notations for producing the plural form of a name, based on the singular form.</summary>
public static class CommonPluralNotation
{
    /// <summary>Uses the unmodified singular form.</summary>
    public const string Unmodified = "[*]";

    /// <summary>Appends { s } to the singular form.</summary>
    /// <remarks>For example, { <i>Metre</i> → <i>Metres</i> }.</remarks>
    public const string AppendS = "[*]s";

    /// <summary>Appends { es } to the singular form.</summary>
    /// <remarks>For example, { <i>Inch</i> → <i>Inches</i> }.</remarks>
    public const string AppendEs = "[*]es";

    /// <summary>Inserts { s } before the first occcurence of { Per } in the singular form.</summary>
    /// <remarks>For example, { <i>MetrePerSecond</i> → <i>MetresPerSecond</i> }.</remarks>
    public const string InsertSBeforePer = "s[Per]";

    /// <summary>Inserts { es } before the first occcurence of { Per } in the singular form.</summary>
    /// <remarks>For example, { <i>InchPerSecond</i> → <i>InchesPerSecond</i> }.</remarks>
    public const string InsertEsBeforePer = "es[Per]";

    /// <summary>Appends { Multiples } to the singular form.</summary>
    /// <remarks>For example, { <i>StandardGravity</i> → <i>StandardGravityMultiples</i> }.</remarks>
    public const string AppendMultiples = "[*]Multiples";

    /// <summary>Prepends { MultiplesOf } to the singular form.</summary>
    /// <remarks>For example, { <i>StandardGravity</i> → <i>MultiplesOfStandardGravity</i> }.</remarks>
    public const string PrependMultiplesOf = "MultiplesOf[*]";

    /// <summary>Prepends { In } to the singular form.</summary>
    /// <remarks>For example, { <i>StandardGravity</i> → <i>InStandardGravity</i> }.</remarks>
    public const string PrependIn = "In[*]";

    /// <summary>Prepends { In } and appends { Multiples } to the singular form.</summary>
    /// <remarks>For example, { <i>StandardGravity</i> → <i>InStandardGravityMultiples</i> }.</remarks>
    public const string PrependInAndAppendMultiples = "In[*]Multiples";

    /// <summary>Prepends { InMultiplesOf } to the singular form.</summary>
    /// <remarks>For example, { <i>StandardGravity</i> → <i>InMultiplesOfStandardGravity</i> }.</remarks>
    public const string PrependInMultiplesOf = "InMultiplesOf[*]";

    /// <summary>Prepends { In } and appends { s } to the singular form.</summary>
    /// <remarks>For example, { <i>PlanckLength</i> → <i>InPlanckLengths</i> }.</remarks>
    public const string PrependInAndAppendS = "In[*]s";

    /// <summary>Prepends { In } and appends { es } to the singular form.</summary>
    /// <remarks>For example, { <i>PlanckMass</i> → <i>InPlanckMasses</i> }.</remarks>
    public const string PrependInAndAppendEs = "In[*]es";
}
