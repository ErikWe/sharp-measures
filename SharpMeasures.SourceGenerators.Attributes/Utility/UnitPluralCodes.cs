namespace ErikWe.SharpMeasures.Attributes.Utility;

/// <summary>Some common notation for producing the plural form of the name of a unit.</summary>
public static class UnitPluralCodes
{
    /// <summary>Uses the unmodified singular form.</summary>
    public const string Unmodified = "=";
    /// <summary>Appends a lower-case "s" to the singular form.</summary>
    /// <remarks>For example; the singular form "Metre" results in the plural form "Metres".</remarks>
    public const string AppendS = "+s";
    /// <summary>Appends the lower-cased "es" to the singular form.</summary>
    /// <remarks>For example; the singular form "Inch" results in the plural form "Inches".</remarks>
    public const string AppendEs = "+es";
    /// <summary>Inserts a lower-case "s" before the first occcurence of "Per" in the singular form.</summary>
    /// <remarks>For example; the singular form "MetrePerSecond" results in the plural form "MetresPerSecond".</remarks>
    public const string InsertSBeforePer = "+s [Per]";
    /// <summary>Inserts the lower-cased "es" before the first occcurence of "Per" in the singular form.</summary>
    /// <remarks>For example; the singular form "InchPerSecond" results in the plural form "InchesPerSecond".</remarks>
    public const string InsertEsBeforePer = "+es [Per]";
}
