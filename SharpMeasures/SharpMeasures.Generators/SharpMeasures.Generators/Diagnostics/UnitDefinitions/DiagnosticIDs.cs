namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string InvalidUnitName = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}00";
    public const string DuplicateUnitName = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}01";
    public const string UnitNameNotRecognized = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}02";
    public const string InvalidUnitPluralForm = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}03";

    public const string DerivedUnitSignatureNotRecognized = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}04";
    public const string UnitListNotMatchingSignature = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}05";

    public const string UndefinedPrefix = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.UnitDefinitions}06";
}
