namespace SharpMeasures.Generators.Units;

internal static class UnitDocumentationTags
{
    public const string UnitHeader = nameof(UnitHeader);

    public const string Quantity = nameof(Quantity);
    public const string Offset = nameof(Offset);

    public const string Constructor = nameof(Constructor);
    new public const string ToString = nameof(ToString);

    public const string ScaledBy_Scalar = nameof(ScaledBy_Scalar);
    public const string ScaledBy_Double = nameof(ScaledBy_Double);

    public const string OffsetBy_Scalar = nameof(OffsetBy_Scalar);
    public const string OffsetBy_Double = nameof(OffsetBy_Double);

    public const string WithPrefix_Metric = nameof(WithPrefix_Metric);
    public const string WithPrefix_Binary = nameof(WithPrefix_Binary);

    public static class Comparable
    {
        public const string CompareTo_SameType = nameof(CompareTo_SameType);

        public static class Operators
        {
            public const string LessThan_SameType = $"Operator_{nameof(LessThan_SameType)}";
            public const string GreaterThan_SameType = $"Operator_{nameof(GreaterThan_SameType)}";
            public const string LessThanOrEqual_SameType = $"Operator_{nameof(LessThanOrEqual_SameType)}";
            public const string GreaterThanOrEqual_SameType = $"Operator_{nameof(GreaterThanOrEqual_SameType)}";
        }
    }

    public static class Derivable
    {
        public static string WithSignature(string signature) => $"From_{signature}";
    }

    public static class Definition
    {
        public static string WithName(string name) => $"Definition_{name}";
    }
}
