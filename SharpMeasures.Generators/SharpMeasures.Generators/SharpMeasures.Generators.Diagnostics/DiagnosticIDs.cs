namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    private static class Numbering
    {
        public static class Thousands
        {
            public const string SourceGenerators = "1";
        }

        public static class Hundreds
        {
            public const string Misc = $"{Thousands.SourceGenerators}0";
            public const string Units = $"{Thousands.SourceGenerators}1";
            public const string Quantities = $"{Thousands.SourceGenerators}2";
            public const string Scalars = $"{Thousands.SourceGenerators}3";
            public const string Vectors = $"{Thousands.SourceGenerators}4";
            public const string Documentation = $"{Thousands.SourceGenerators}9";
        }
    }

    private const string Prefix = "Measures";

    public const string TypeNotPartial = $"{Prefix}{Numbering.Hundreds.Misc}00";
    public const string TypeNotScalar = $"{Prefix}{Numbering.Hundreds.Misc}01";
    public const string TypeNotVector = $"{Prefix}{Numbering.Hundreds.Misc}02";
    public const string TypeNotVectorGroup = $"{Prefix}{Numbering.Hundreds.Misc}03";
    public const string TypeNotVectorGroupMember = $"{Prefix}{Numbering.Hundreds.Misc}04";
    public const string TypeNotQuantity = $"{Prefix}{Numbering.Hundreds.Misc}05";
    public const string TypeNotUnit = $"{Prefix}{Numbering.Hundreds.Misc}06";
    public const string TypeAlreadyDefined = $"{Prefix}{Numbering.Hundreds.Misc}07";

    public const string TypeNotUnbiasedScalar = $"{Prefix}{Numbering.Hundreds.Misc}20";
    public const string TypeNotBiasedScalar = $"{Prefix}{Numbering.Hundreds.Misc}21";
    public const string UnitNotIncludingBiasTerm = $"{Prefix}{Numbering.Hundreds.Misc}22";

    public const string InvalidDerivationExpression = $"{Prefix}{Numbering.Hundreds.Misc}40";
    public const string InvalidDerivationSignature = $"{Prefix}{Numbering.Hundreds.Misc}41";

    public const string EmptyList = $"{Prefix}{Numbering.Hundreds.Misc}80";
    public const string DuplicateListing = $"{Prefix}{Numbering.Hundreds.Misc}81";
}
