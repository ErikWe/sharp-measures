namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string InvalidVectorDimension = $"{Prefix}{Numbering.Hundreds.Vectors}20";
    public const string MissingVectorDimension = $"{Prefix}{Numbering.Hundreds.Vectors}21";
    public const string DuplicateVectorDimension = $"{Prefix}{Numbering.Hundreds.Vectors}22";

    public const string VectorConstantInvalidDimension = $"{Prefix}{Numbering.Hundreds.Vectors}40";

    public const string VectorGroupAlreadySpecified = $"{Prefix}{Numbering.Hundreds.Vectors}60";
}
