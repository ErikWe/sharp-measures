namespace SharpMeasures.Generators.Utility;

/// <summary>Defines some common notations for producing the plural form of a name, based on the singular form.</summary>
public static class CommonPluralNotations
{
    /// <summary>Uses the unmodified singular form.</summary>
    public const string Unmodified = "[*]";
    /// <summary>Appends the lower-cased "s" to the singular form.</summary>
    /// <remarks>For example; a singular form "Metre" would have the plural form "Metres".</remarks>
    public const string AppendS = "[*]s";
    /// <summary>Appends the lower-cased "es" to the singular form.</summary>
    /// <remarks>For example; a singular form "Inch" would have the plural form "Inches".</remarks>
    public const string AppendEs = "[*]es";
    /// <summary>Inserts the lower-cased "s" before the first occcurence of "Per" in the singular form.</summary>
    /// <remarks>For example; a singular form "MetrePerSecond" would have the plural form "MetresPerSecond".</remarks>
    public const string InsertSBeforePer = "s[Per]";
    /// <summary>Inserts the lower-cased "es" before the first occcurence of "Per" in the singular form.</summary>
    /// <remarks>For example; a singular form "InchPerSecond" would have the plural form "InchesPerSecond".</remarks>
    public const string InsertEsBeforePer = "es[Per]";
}
