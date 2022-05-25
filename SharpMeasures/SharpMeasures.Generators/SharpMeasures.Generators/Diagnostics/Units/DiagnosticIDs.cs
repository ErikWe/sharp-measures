namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string InvalidUnitName = $"{Prefix}{Numbering.Hundreds.Units}00";
    public const string InvalidUnitPluralForm = $"{Prefix}{Numbering.Hundreds.Units}01";
    public const string DuplicateUnitName = $"{Prefix}{Numbering.Hundreds.Units}02";
    public const string DuplicateUnitPluralForm = $"{Prefix}{Numbering.Hundreds.Units}03";
    public const string UnrecognizedUnitName = $"{Prefix}{Numbering.Hundreds.Units}04";
    public const string CyclicUnitDependency = $"{Prefix}{Numbering.Hundreds.Units}05";

    public const string UnrecognizedPrefix = $"{Prefix}{Numbering.Hundreds.Units}20";

    public const string InvalidUnitDerivationExpression = $"{Prefix}{Numbering.Hundreds.Units}40";
    public const string EmptyUnitDerivationSignature = $"{Prefix}{Numbering.Hundreds.Units}41";
    public const string DuplicateUnitDerivationSignature = $"{Prefix}{Numbering.Hundreds.Units}42";
    public const string UnrecognizedDerivedUnitSignature = $"{Prefix}{Numbering.Hundreds.Units}43";
    public const string UnitListNotMatchingSignature = $"{Prefix}{Numbering.Hundreds.Units}44";
}
