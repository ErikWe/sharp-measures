namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string DefineQuantityUnitAndSymbol = $"{Prefix}{Numbering.Hundreds.Quantities}00";

    public const string InvalidConstantName = $"{Prefix}{Numbering.Hundreds.Quantities}20";
    public const string InvalidConstantMultiplesName = $"{Prefix}{Numbering.Hundreds.Quantities}21";
    public const string DuplicateConstantName = $"{Prefix}{Numbering.Hundreds.Quantities}22";
    public const string ConstantSharesNameWithUnit = $"{Prefix}{Numbering.Hundreds.Quantities}23";
    public const string ConstantMultiplesDisabledButNameSpecified = $"{Prefix}{Numbering.Hundreds.Quantities}24";

    public const string UnrecognizedCastOperatorBehaviour = $"{Prefix}{Numbering.Hundreds.Quantities}40";
    public const string QuantityConvertibleToSelf = $"{Prefix}{Numbering.Hundreds.Quantities}41";

    public const string ContradictoryAttributes = $"{Prefix}{Numbering.Hundreds.Quantities}60";
    public const string QuantityGroupMissingRoot = $"{Prefix}{Numbering.Hundreds.Quantities}61";
    public const string InclusionOrExclusionHadNoEffect = $"{Prefix}{Numbering.Hundreds.Quantities}62";
    public const string DifferenceDisabledButQuantitySpecified = $"{Prefix}{Numbering.Hundreds.Quantities}63";
}
