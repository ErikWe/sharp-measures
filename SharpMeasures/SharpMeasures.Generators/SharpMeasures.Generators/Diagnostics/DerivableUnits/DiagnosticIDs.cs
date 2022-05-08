namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string InvalidUnitDerivationExpression = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Derivable}00";
    public const string EmptyUnitDerivationSignature = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Derivable}01";
    public const string DuplicateUnitDerivationSignature = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Derivable}02";
}
