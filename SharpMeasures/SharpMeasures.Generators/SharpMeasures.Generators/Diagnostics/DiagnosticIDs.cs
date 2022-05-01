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
            public const string Misc = "0";
            public const string Derivable = "1";
            public const string UnitDefinitions = "2";
        }
    }

    private const string Prefix = "Measure";

    public const string TypeNotPartial = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Misc}00";
    public const string TypeNotScalarQuantity = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Misc}01";
    public const string TypeNotUnbiasedScalarQuantity = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Misc}02";
    public const string TypeNotUnit = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Misc}03";
}
