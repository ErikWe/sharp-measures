namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string InvalidVectorDimension = $"{Prefix}{Numbering.Hundreds.Vectors}20";
    public const string MissingVectorDimension = $"{Prefix}{Numbering.Hundreds.Vectors}21";
    public const string VectorUnexpectedDimension = $"{Prefix}{Numbering.Hundreds.Vectors}22";
    public const string DuplicateVectorDimension = $"{Prefix}{Numbering.Hundreds.Vectors}23";
    public const string VectorGroupLacksMemberOfDimension = $"{Prefix}{Numbering.Hundreds.Vectors}24";
    public const string VectorNameAndDimensionConflict = $"{Prefix}{Numbering.Hundreds.Vectors}25";
    public const string VectorGroupNameSuggestsDimension = $"{Prefix}{Numbering.Hundreds.Vectors}26";
    public const string NonOverlappingVectorDimensions = $"{Prefix}{Numbering.Hundreds.Vectors}27";

    public const string VectorConstantInvalidDimension = $"{Prefix}{Numbering.Hundreds.Vectors}40";

    public const string VectorNotSupportingCrossMultiplication = $"{Prefix}{Numbering.Hundreds.Vectors}60";
}
