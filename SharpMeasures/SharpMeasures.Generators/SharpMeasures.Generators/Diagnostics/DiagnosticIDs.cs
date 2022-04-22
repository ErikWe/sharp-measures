namespace SharpMeasures.Generators.Diagnostics;

public static class DiagnosticIDs
{
    public const string Prefix = "Measure";

    public const string TypeNotPartial = $"{Prefix}1000";
    public const string TypeNotScalarQuantity = $"{Prefix}1001";
    public const string TypeNotUnbiasedScalarQuantity = $"{Prefix}1002";
}
