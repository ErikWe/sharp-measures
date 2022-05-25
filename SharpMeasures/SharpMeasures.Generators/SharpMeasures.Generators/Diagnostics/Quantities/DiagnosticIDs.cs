namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string InvalidConstantName = $"{Prefix}{Numbering.Hundreds.Quantities}00";
    public const string InvalidConstantMultiplesName = $"{Prefix}{Numbering.Hundreds.Quantities}01";
    public const string DuplicateConstantName = $"{Prefix}{Numbering.Hundreds.Quantities}02";
    public const string DuplicateConstantMultiplesName = $"{Prefix}{Numbering.Hundreds.Quantities}03";

    public const string ExcessiveExclusion = $"{Prefix}{Numbering.Hundreds.Quantities}60";
}
